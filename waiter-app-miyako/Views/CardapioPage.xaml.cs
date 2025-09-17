using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace waiter_app_miyako.Views
{
    public partial class CardapioPage : ContentPage
    {
        // ===== Configurações em porcentagem (0.0 a 1.0) =====
        private double _minPct = 0.12;         // Minimizada (~12% da altura disponível)
        private double _halfPct = 0.50;        // Metade da tela
        private double _maxPct = 1.00;         // Cheia (até o topo, sem gap)
        private double _snapThresholdPct = 0.07; // Limiar de "imã" (7% da altura)

        // Altura em px calculada dinamicamente a partir das porcentagens
        private double _minHeight, _halfHeight, _maxHeight, _snapThreshold;

        // Estado e controle do gesto
        private double _alturaInicialArraste;

        private enum SheetState { Minimizada, Metade, Expandida }
        private SheetState _estadoAtual = SheetState.Minimizada;

        public CardapioPage()
        {
            InitializeComponent();
            MainGrid.SizeChanged += MainGrid_SizeChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RecalcularAncoras();
            // Garante início minimizado baseado em porcentagem
            SnapTo(SheetState.Minimizada, animated: false);
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

        // ============== Seus handlers originais ==============
        private async void OnNavegarParaSessaoClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var targetElement = button.CommandParameter as VisualElement;
            if (targetElement == null) return;

            await MenuScrollView.ScrollToAsync(targetElement, ScrollToPosition.Start, true);
        }

        private async void OnItemTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new ItemDetalhesPage());
        }
        // =====================================================

        // ============== Gesto de arraste (Pan) ===============
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
                    // Arrastar para cima => TotalY negativo => aumenta a altura
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
        // =====================================================

        // ================= Snap/Âncoras (% tela) =============
        private void RecalcularAncoras()
        {
            double total = MainGrid?.Height ?? 0;
            if (total <= 0) return;

            _minHeight = total * _minPct;
            _halfHeight = total * _halfPct;
            _maxHeight = total * _maxPct;            // 100% da tela
            _snapThreshold = total * _snapThresholdPct;
        }

        private void SnapSmart(double alturaSolta)
        {
            var delta = alturaSolta - _alturaInicialArraste;
            var anchors = Anchors();

            // Movimento grande: vá para a próxima âncora na direção do arraste
            if (Math.Abs(delta) > _snapThreshold)
            {
                double target;
                if (delta > 0)
                {
                    // Puxou pra cima: próxima âncora acima
                    target = anchors.Where(a => a > _alturaInicialArraste).DefaultIfEmpty(_maxHeight).Min();
                }
                else
                {
                    // Puxou pra baixo: próxima âncora abaixo
                    target = anchors.Where(a => a < _alturaInicialArraste).DefaultIfEmpty(_minHeight).Max();
                }

                SnapTo(ClosestState(target));
            }
            else
            {
                // Movimento curto: imã para a âncora mais próxima
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
        // =====================================================

        // =================== Utilitários =====================
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
        // =====================================================
    }
}
