using waiter_app_miyako.ViewModels;
using System.Linq;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.Views
{
    public partial class CardapioPage : ContentPage
    {
        // ===== Configurações em porcentagem (0.0 a 1.0) =====
        private double _minPct = 0.12;         // Minimizada
        private double _halfPct = 0.50;        // Metade da tela
        private double _maxPct = 1.00;         // Até o topo
        private double _snapThresholdPct = 0.07; // Limiar de "imã"

        // Altura em px calculada dinamicamente a partir das porcentagens
        private double _minHeight, _halfHeight, _maxHeight, _snapThreshold;

        // Estado e controle do gesto
        private double _alturaInicialArraste;

        private enum SheetState { Minimizada, Metade, Expandida }
        private SheetState _estadoAtual = SheetState.Minimizada;

        // ViewModel para gerenciar os dados do cardápio
        private readonly CardapioViewModel _viewModel;

        public CardapioPage()
        {
            InitializeComponent();
            _viewModel = new CardapioViewModel();
            this.BindingContext = _viewModel; // Define o BindingContext da página
            MainGrid.SizeChanged += MainGrid_SizeChanged;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Só carrega os dados se a lista estiver vazia, para manter o estado do pedido
            if (!_viewModel.CardapioAgrupado.Any())
            {
                await _viewModel.CarregarDadosIniciais();
            }

            RecalcularAncoras();

            // Evita que o BottomSheet minimize ao voltar de outra página se já estava aberto
            if (_estadoAtual == SheetState.Minimizada)
            {
                SnapTo(SheetState.Minimizada, animated: false);
            }
        }

        protected override void OnDisappearing()
        {
            MainGrid.SizeChanged -= MainGrid_SizeChanged;
            base.OnDisappearing();
        }

        private void MainGrid_SizeChanged(object sender, EventArgs e)
        {
            var estadoAntes = _estadoAtual;
            RecalcularAncoras();
            SnapTo(estadoAntes, animated: false);
        }

        private void OnExpandirClicked(object sender, EventArgs e)
        {
            SnapTo(SheetState.Expandida);
        }

        private void OnMinimizarClicked(object sender, EventArgs e)
        {
            SnapTo(SheetState.Minimizada);
        }

        private void OnNavegarParaSessaoClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is string nomeDoGrupo)
            {
                var groupIndex = _viewModel.CardapioAgrupado
                    .Select((g, idx) => new { g, idx })
                    .FirstOrDefault(x => string.Equals(x.g.NomeDoGrupo, nomeDoGrupo, StringComparison.OrdinalIgnoreCase))?.idx ?? -1;

                if (groupIndex < 0) return;

                MenuCollectionView.ScrollTo(0, groupIndex, ScrollToPosition.Start, true);
            }
        }

        private async void OnItemTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is Models.Produtos produto)
            {
                await Navigation.PushAsync(new ItemDetalhesPage(produto, AdicionarItemAoPedido));
            }
        }

        private void AdicionarItemAoPedido(Produtos produto, int quantidade)
        {
            var itemExistente = _viewModel.ItensDoPedido.FirstOrDefault(i => i.Produto.id == produto.id);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                _viewModel.ItensDoPedido.Add(new ItemPedidoViewModel(produto, quantidade));
            }

            SnapTo(SheetState.Expandida);
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (MainGrid.Height <= 0) return;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    this.AbortAnimation("BottomSheetAnim");
                    RecalcularAncoras();
                    _alturaInicialArraste = BottomSheetRow.Height.Value;
                    break;

                case GestureStatus.Running:
                    var novaAltura = _alturaInicialArraste - e.TotalY;
                    novaAltura = Math.Clamp(novaAltura, _minHeight, _maxHeight);
                    SetHeight(novaAltura);
                    break;

                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    var alturaFinal = BottomSheetRow.Height.Value;
                    SnapSmart(alturaFinal);
                    break;
            }
        }

        private void RecalcularAncoras()
        {
            double total = MainGrid?.Height ?? 0;
            if (total <= 0) return;

            _minHeight = total * _minPct;
            _halfHeight = total * _halfPct;
            _maxHeight = total * _maxPct;
            _snapThreshold = total * _snapThresholdPct;
        }

        private void SnapSmart(double alturaSolta)
        {
            var delta = alturaSolta - _alturaInicialArraste;
            var anchors = Anchors();

            if (Math.Abs(delta) > _snapThreshold)
            {
                double target;
                if (delta > 0)
                {
                    target = anchors.Where(a => a > _alturaInicialArraste).DefaultIfEmpty(_maxHeight).Min();
                }
                else
                {
                    target = anchors.Where(a => a < _alturaInicialArraste).DefaultIfEmpty(_minHeight).Max();
                }
                SnapTo(ClosestState(target));
            }
            else
            {
                SnapToNearest(alturaSolta);
            }
        }

        private void SnapToNearest(double alturaAtual, bool animated = true)
        {
            var anchors = Anchors();
            var states = new[] { SheetState.Minimizada, SheetState.Metade, SheetState.Expandida };

            int idxMelhor = 0;
            double melhor = double.MaxValue;

            for (int i = 0; i < anchors.Length; i++)
            {
                var dist = Math.Abs(alturaAtual - anchors[i]);
                if (dist < melhor)
                {
                    melhor = dist;
                    idxMelhor = i;
                }
            }

            SnapTo(states[idxMelhor], animated);
        }

        private void SnapTo(SheetState state, bool animated = true)
        {
            double destino = state switch
            {
                SheetState.Minimizada => _minHeight,
                SheetState.Metade => _halfHeight,
                SheetState.Expandida => _maxHeight,
                _ => _minHeight
            };

            if (animated)
                AnimateHeight(BottomSheetRow.Height.Value, destino, 200, Easing.CubicOut);
            else
                SetHeight(destino);

            _estadoAtual = state;
        }

        private SheetState ClosestState(double altura)
        {
            var anchors = Anchors();
            var states = new[] { SheetState.Minimizada, SheetState.Metade, SheetState.Expandida };
            int idx = 0;
            double best = double.MaxValue;
            for (int i = 0; i < anchors.Length; i++)
            {
                var d = Math.Abs(altura - anchors[i]);
                if (d < best)
                {
                    best = d;
                    idx = i;
                }
            }
            return states[idx];
        }

        private double[] Anchors() => new[] { _minHeight, _halfHeight, _maxHeight };

        private void SetHeight(double h)
        {
            BottomSheetRow.Height = new GridLength(h);
        }

        private void AnimateHeight(double from, double to, uint length, Easing easing)
        {
            this.AbortAnimation("BottomSheetAnim");
            var anim = new Animation(v => BottomSheetRow.Height = new GridLength(v), from, to);
            anim.Commit(this, "BottomSheetAnim", length: length, easing: easing ?? Easing.CubicOut);
        }
    }
}

