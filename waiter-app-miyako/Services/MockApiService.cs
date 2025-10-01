using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using waiter_app_miyako.Models;

namespace waiter_app_miyako.Services
{
    public class MockApiService
    {
        // Simula a latência da rede
        private async Task Delay(int ms) => await Task.Delay(ms);

        // Retorna os dados do FUNCIONÁRIO LOGADO
        public async Task<Funcionarios> FetchFuncionarioLogado()
        {
            await Delay(300);
            return new Funcionarios
            {
                funcionario = "Carlos Eduardo",
                foto = null,
                genero = "Masculino",
                matricula = "987654",
                cargo = "Garçom"
            };
        }

        // Retorna os dados das MESAS
        public async Task<List<Mesas>> FetchMesas()
        {
            await Delay(500);
            return new List<Mesas>
            {
                new Mesas { numeroMesa = 1, statusMesa = "atencao" },
                new Mesas { numeroMesa = 2, statusMesa = "livre" },
                new Mesas { numeroMesa = 3, statusMesa = "atencao" },
                new Mesas { numeroMesa = 4, statusMesa = "livre" },
                new Mesas { numeroMesa = 5, statusMesa = "ocupada" },
                new Mesas { numeroMesa = 6, statusMesa = "ocupada" },
                new Mesas { numeroMesa = 7, statusMesa = "atencao" },
                new Mesas { numeroMesa = 8, statusMesa = "livre" },
                new Mesas { numeroMesa = 9, statusMesa = "livre" },
                new Mesas { numeroMesa = 10, statusMesa = "livre" },
                new Mesas { numeroMesa = 11, statusMesa = "livre" },
                new Mesas { numeroMesa = 12, statusMesa = "livre" },
                new Mesas { numeroMesa = 13, statusMesa = "livre" },
                new Mesas { numeroMesa = 14, statusMesa = "livre" },
                new Mesas { numeroMesa = 15, statusMesa = "livre" },
                new Mesas { numeroMesa = 16, statusMesa = "livre" },
                new Mesas { numeroMesa = 17, statusMesa = "livre" },
                new Mesas { numeroMesa = 18, statusMesa = "livre" },
                new Mesas { numeroMesa = 19, statusMesa = "livre" },
                new Mesas { numeroMesa = 20, statusMesa = "livre" },
            };
        }

        // Retorna os dados dos PEDIDOS (Atualizado com os novos dados)
        public async Task<List<Pedidos>> FetchPedidos()
        {
            await Delay(700);
            var funcionario = await FetchFuncionarioLogado();
            var produtos = await FetchProdutos();

            var pedidos = new List<Pedidos>
            {
                new Pedidos {
                    id = 1,
                    mesaNumero = 8,
                    clientes = 3,
                    mesa = new Mesas { numeroMesa = 8 },
                    funcionario = funcionario,
                    observacao = "Cliente pediu guardanapos extras.",
                    dataAberturaPedido = new DateTime(2025, 10, 01, 20, 30, 0),
                    finalizado = false,
                    itens = new List<ItensPedidos> {
                        new ItensPedidos { produto = produtos.First(p => p.id == 401), quantidade = 2, Status = "aberto" },
                        new ItensPedidos { produto = produtos.First(p => p.id == 201), quantidade = 4, Status = "aberto" }
                    }
                },
                new Pedidos {
                    id = 2,
                    mesaNumero = 10,
                    clientes = 5,
                    mesa = new Mesas { numeroMesa = 10 },
                    funcionario = funcionario,
                    observacao = "Sem observações.",
                    dataAberturaPedido = new DateTime(2025, 10, 01, 20, 0, 0),
                    finalizado = false,
                    itens = new List<ItensPedidos> {
                        new ItensPedidos { produto = produtos.First(p => p.id == 102), quantidade = 1, Status = "aberto" }
                    }
                },
                new Pedidos {
                    id = 3,
                    mesaNumero = 3,
                    clientes = 1,
                    mesa = new Mesas { numeroMesa = 3 },
                    funcionario = funcionario,
                    observacao = "Cliente alérgico a camarão.",
                    dataAberturaPedido = new DateTime(2025, 10, 01, 19, 0, 0),
                    dataConclusaoPedido = new DateTime(2025, 9, 30, 20, 0, 0),
                    finalizado = true,
                    itens = new List<ItensPedidos> {
                        new ItensPedidos { produto = produtos.First(p => p.id == 301), quantidade = 1, Status = "entregue" },
                        new ItensPedidos { produto = produtos.First(p => p.id == 206), quantidade = 2, Status = "entregue" }
                    }
                },
            };
            return pedidos.OrderByDescending(p => p.dataAberturaPedido).ToList();
        }

        // Retorna as categorias do cardápio
        public async Task<List<Grupos>> FetchCategorias()
        {
            await Delay(500);
            return new List<Grupos>
            {
                new Grupos { id = 1, descricao = "Barcas" },
                new Grupos { id = 2, descricao = "Sushis" },
                new Grupos { id = 3, descricao = "Pratos Quentes" },
                new Grupos { id = 4, descricao = "Bebidas" },
            };
        }

        // Retorna os produtos do cardápio (Atualizado com a nova lista)
        public async Task<List<Produtos>> FetchProdutos()
        {
            await Delay(800);
            return new List<Produtos>
            {
                // Grupo 1 - Barcas
                new Produtos { id = 101, descricao = "Barca Miyako (2 Pessoas)", preco = 120.00m, grupoId = 1, ingredientes = "Seleção de sushis e sashimis variados.", tempoPreparo = 25, estoque = 10 },
                new Produtos { id = 102, descricao = "Barca Salmão (1 Pessoa)", preco = 75.00m, grupoId = 1, ingredientes = "Sashimi, niguiri e uramaki de salmão.", tempoPreparo = 15, estoque = 15 },
                new Produtos { id = 103, descricao = "Barca Família (4 Pessoas)", preco = 220.00m, grupoId = 1, ingredientes = "Grande variedade de sushis, sashimis e pratos quentes.", tempoPreparo = 35, estoque = 8 },
                new Produtos { id = 104, descricao = "Barca Premium (2 Pessoas)", preco = 160.00m, grupoId = 1, ingredientes = "Itens especiais do chef, incluindo iguarias.", tempoPreparo = 30, estoque = 7 },
                new Produtos { id = 105, descricao = "Barca de Atum (2 Pessoas)", preco = 130.00m, grupoId = 1, ingredientes = "Sashimi, niguiri e uramaki de atum.", tempoPreparo = 20, estoque = 12 },
                new Produtos { id = 106, descricao = "Barca Mista (1 Pessoa)", preco = 80.00m, grupoId = 1, ingredientes = "Mix de salmão, atum e peixe branco.", tempoPreparo = 18, estoque = 20 },
                new Produtos { id = 107, descricao = "Barca Especial (3 Pessoas)", preco = 180.00m, grupoId = 1, ingredientes = "Seleção premium com foco em sashimis.", tempoPreparo = 28, estoque = 9 },
                new Produtos { id = 108, descricao = "Barca Executiva (1 Pessoa)", preco = 70.00m, grupoId = 1, ingredientes = "Opção rápida com itens populares.", tempoPreparo = 12, estoque = 25 },
                new Produtos { id = 109, descricao = "Barca Festival (2 Pessoas)", preco = 150.00m, grupoId = 1, ingredientes = "Itens do festival de sushi.", tempoPreparo = 22, estoque = 11 },
                new Produtos { id = 110, descricao = "Barca do Chef (3 Pessoas)", preco = 200.00m, grupoId = 1, ingredientes = "Criações exclusivas do nosso sushiman.", tempoPreparo = 32, estoque = 6 },

                // Grupo 2 - Sushis
                new Produtos { id = 401, descricao = "Uramaki Filadélfia (8 un)", preco = 32.00m, grupoId = 2, ingredientes = "Arroz, alga, salmão, cream cheese e cebolinha.", tempoPreparo = 10, estoque = 30 },
                new Produtos { id = 402, descricao = "Sashimi de Atum (5 fatias)", preco = 28.00m, grupoId = 2, ingredientes = "Fatias frescas de atum.", tempoPreparo = 5, estoque = 40 },
                new Produtos { id = 403, descricao = "Nigiri de Salmão (2 un)", preco = 18.00m, grupoId = 2, ingredientes = "Arroz coberto com uma fatia de salmão fresco.", tempoPreparo = 8, estoque = 50 },
                new Produtos { id = 404, descricao = "Nigiri de Camarão (2 un)", preco = 22.00m, grupoId = 2, ingredientes = "Arroz coberto com camarão cozido.", tempoPreparo = 10, estoque = 35 },
                new Produtos { id = 405, descricao = "Hossomaki de Pepino (8 un)", preco = 20.00m, grupoId = 2, ingredientes = "Arroz e pepino enrolados em alga.", tempoPreparo = 7, estoque = 45 },
                new Produtos { id = 406, descricao = "Hossomaki de Salmão (8 un)", preco = 28.00m, grupoId = 2, ingredientes = "Arroz e salmão enrolados em alga.", tempoPreparo = 9, estoque = 38 },
                new Produtos { id = 407, descricao = "Temaki Salmão com Cream Cheese", preco = 30.00m, grupoId = 2, ingredientes = "Cone de alga com arroz, salmão, cream cheese e cebolinha.", tempoPreparo = 12, estoque = 28 },
                new Produtos { id = 408, descricao = "Temaki Atum Spicy", preco = 32.00m, grupoId = 2, ingredientes = "Cone de alga com arroz, atum e molho picante.", tempoPreparo = 13, estoque = 26 },
                new Produtos { id = 409, descricao = "Sashimi de Salmão (5 fatias)", preco = 26.00m, grupoId = 2, ingredientes = "Fatias frescas de salmão.", tempoPreparo = 5, estoque = 42 },
                new Produtos { id = 410, descricao = "Uramaki Califórnia (8 un)", preco = 30.00m, grupoId = 2, ingredientes = "Arroz, alga, manga, pepino e kani.", tempoPreparo = 11, estoque = 33 },

                // Grupo 3 - Pratos Quentes
                new Produtos { id = 301, descricao = "Yakisoba de Carne", preco = 45.00m, grupoId = 3, ingredientes = "Macarrão, carne e legumes ao molho especial.", tempoPreparo = 15, estoque = 20 },
                new Produtos { id = 302, descricao = "Tempurá de Camarão", preco = 55.00m, grupoId = 3, ingredientes = "Camarões e legumes empanados e fritos.", tempoPreparo = 18, estoque = 18 },
                new Produtos { id = 303, descricao = "Lamen Tradicional", preco = 42.00m, grupoId = 3, ingredientes = "Caldo à base de porco, macarrão, ovo e vegetais.", tempoPreparo = 20, estoque = 15 },
                new Produtos { id = 304, descricao = "Teppan de Salmão", preco = 68.00m, grupoId = 3, ingredientes = "Salmão grelhado na chapa com legumes.", tempoPreparo = 22, estoque = 12 },
                new Produtos { id = 305, descricao = "Katsu Don (Carne de Porco)", preco = 50.00m, grupoId = 3, ingredientes = "Carne de porco empanada sobre arroz e ovo.", tempoPreparo = 17, estoque = 16 },
                new Produtos { id = 306, descricao = "Chicken Karaage", preco = 48.00m, grupoId = 3, ingredientes = "Frango frito marinado no estilo japonês.", tempoPreparo = 16, estoque = 19 },
                new Produtos { id = 307, descricao = "Gyoza de Carne (6 un)", preco = 38.00m, grupoId = 3, ingredientes = "Pastéis japoneses recheados com carne.", tempoPreparo = 14, estoque = 22 },
                new Produtos { id = 308, descricao = "Yakisoba de Frango", preco = 43.00m, grupoId = 3, ingredientes = "Macarrão, frango e legumes ao molho especial.", tempoPreparo = 15, estoque = 21 },
                new Produtos { id = 309, descricao = "Missoshiru", preco = 18.00m, grupoId = 3, ingredientes = "Sopa de soja com tofu e cebolinha.", tempoPreparo = 5, estoque = 30 },
                new Produtos { id = 310, descricao = "Hot Filadélfia Empanado", preco = 36.00m, grupoId = 3, ingredientes = "Sushi de salmão e cream cheese, empanado e frito.", tempoPreparo = 13, estoque = 24 },

                // Grupo 4 - Bebidas
                new Produtos { id = 201, descricao = "Coca-Cola Lata", preco = 7.00m, grupoId = 4, ingredientes = "350ml", estoque = 100 },
                new Produtos { id = 202, descricao = "Cerveja Sapporo", preco = 25.00m, grupoId = 4, ingredientes = "Lager japonesa.", estoque = 50 },
                new Produtos { id = 203, descricao = "Água Mineral sem Gás", preco = 5.00m, grupoId = 4, ingredientes = "500ml", estoque = 150 },
                new Produtos { id = 204, descricao = "Água Mineral com Gás", preco = 6.00m, grupoId = 4, ingredientes = "500ml", estoque = 120 },
                new Produtos { id = 205, descricao = "Chá Verde Gelado", preco = 12.00m, grupoId = 4, ingredientes = "Chá verde com um toque de limão.", estoque = 40 },
                new Produtos { id = 206, descricao = "Suco Natural de Laranja", preco = 14.00m, grupoId = 4, ingredientes = "Feito com laranjas frescas.", estoque = 30 },
                new Produtos { id = 207, descricao = "Suco Natural de Abacaxi", preco = 14.00m, grupoId = 4, ingredientes = "Polpa de abacaxi natural.", estoque = 30 },
                new Produtos { id = 208, descricao = "Refrigerante Guaraná Lata", preco = 7.00m, grupoId = 4, ingredientes = "350ml", estoque = 90 },
                new Produtos { id = 209, descricao = "Cerveja Heineken Long Neck", preco = 20.00m, grupoId = 4, ingredientes = "330ml", estoque = 60 },
                new Produtos { id = 210, descricao = "Saquê Tradicional", preco = 30.00m, grupoId = 4, ingredientes = "Dose de saquê nacional.", estoque = 20 },
            };
        }
    }
}