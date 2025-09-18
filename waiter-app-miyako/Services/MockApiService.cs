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

        // Retorna os dados dos PEDIDOS
        public async Task<List<Pedidos>> FetchPedidos()
        {
            await Delay(700);
            var pedidos = new List<Pedidos>
            {
                new Pedidos {
                    id = 1,
                    mesaNumero = 8,
                    mesa = new Mesas { numeroMesa = 8 },
                    status = "aberto", 
                    dataPedido = new DateTime(2025, 9, 18, 20, 30, 0),
                    itens = new List<ItensPedidos> { new ItensPedidos { quantidade = 2 }, new ItensPedidos { quantidade = 4 } }
                },
                new Pedidos {
                    id = 2,
                    mesaNumero = 10,
                    mesa = new Mesas { numeroMesa = 10 },
                    status = "aberto",
                    dataPedido = new DateTime(2025, 9, 18, 20, 0, 0),
                    itens = new List<ItensPedidos> { new ItensPedidos { quantidade = 1 } }
                },
                new Pedidos {
                    id = 3,
                    mesaNumero = 3,
                    mesa = new Mesas { numeroMesa = 3 },
                    status = "entregue", 
                    dataPedido = new DateTime(2025, 9, 18, 19, 0, 0),
                    itens = new List<ItensPedidos> { new ItensPedidos { quantidade = 1 }, new ItensPedidos { quantidade = 2 } }
                },
            };
            return pedidos.OrderByDescending(p => p.dataPedido).ToList();
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

        // Retorna os produtos do cardápio
        public async Task<List<Produtos>> FetchProdutos()
        {
            await Delay(800);
            return new List<Produtos>
            {
                // Grupo 1 - Barcas
                new Produtos { id = 101, descricao = "Barca Miyako (2 Pessoas)", preco = 120.00m, grupoId = 1 },
                new Produtos { id = 102, descricao = "Barca Salmão (1 Pessoa)", preco = 75.00m, grupoId = 1 },
                new Produtos { id = 103, descricao = "Barca Família (4 Pessoas)", preco = 220.00m, grupoId = 1 },
                new Produtos { id = 104, descricao = "Barca Premium (2 Pessoas)", preco = 160.00m, grupoId = 1 },
                new Produtos { id = 105, descricao = "Barca de Atum (2 Pessoas)", preco = 130.00m, grupoId = 1 },
                new Produtos { id = 106, descricao = "Barca Mista (1 Pessoa)", preco = 80.00m, grupoId = 1 },
                new Produtos { id = 107, descricao = "Barca Especial (3 Pessoas)", preco = 180.00m, grupoId = 1 },
                new Produtos { id = 108, descricao = "Barca Executiva (1 Pessoa)", preco = 70.00m, grupoId = 1 },
                new Produtos { id = 109, descricao = "Barca Festival (2 Pessoas)", preco = 150.00m, grupoId = 1 },
                new Produtos { id = 110, descricao = "Barca do Chef (3 Pessoas)", preco = 200.00m, grupoId = 1 },

                // Grupo 2 - Sushis
                new Produtos { id = 401, descricao = "Uramaki Filadélfia (8 un)", preco = 32.00m, grupoId = 2 },
                new Produtos { id = 402, descricao = "Sashimi de Atum (5 fatias)", preco = 28.00m, grupoId = 2 },
                new Produtos { id = 403, descricao = "Nigiri de Salmão (2 un)", preco = 18.00m, grupoId = 2 },
                new Produtos { id = 404, descricao = "Nigiri de Camarão (2 un)", preco = 22.00m, grupoId = 2 },
                new Produtos { id = 405, descricao = "Hossomaki de Pepino (8 un)", preco = 20.00m, grupoId = 2 },
                new Produtos { id = 406, descricao = "Hossomaki de Salmão (8 un)", preco = 28.00m, grupoId = 2 },
                new Produtos { id = 407, descricao = "Temaki Salmão com Cream Cheese", preco = 30.00m, grupoId = 2 },
                new Produtos { id = 408, descricao = "Temaki Atum Spicy", preco = 32.00m, grupoId = 2 },
                new Produtos { id = 409, descricao = "Sashimi de Salmão (5 fatias)", preco = 26.00m, grupoId = 2 },
                new Produtos { id = 410, descricao = "Uramaki Califórnia (8 un)", preco = 30.00m, grupoId = 2 },

                // Grupo 3 - Pratos Quentes
                new Produtos { id = 301, descricao = "Yakisoba de Carne", preco = 45.00m, grupoId = 3 },
                new Produtos { id = 302, descricao = "Tempurá de Camarão", preco = 55.00m, grupoId = 3 },
                new Produtos { id = 303, descricao = "Lamen Tradicional", preco = 42.00m, grupoId = 3 },
                new Produtos { id = 304, descricao = "Teppan de Salmão", preco = 68.00m, grupoId = 3 },
                new Produtos { id = 305, descricao = "Katsu Don (Carne de Porco)", preco = 50.00m, grupoId = 3 },
                new Produtos { id = 306, descricao = "Chicken Karaage", preco = 48.00m, grupoId = 3 },
                new Produtos { id = 307, descricao = "Gyoza de Carne (6 un)", preco = 38.00m, grupoId = 3 },
                new Produtos { id = 308, descricao = "Yakisoba de Frango", preco = 43.00m, grupoId = 3 },
                new Produtos { id = 309, descricao = "Missoshiru", preco = 18.00m, grupoId = 3 },
                new Produtos { id = 310, descricao = "Hot Filadélfia Empanado", preco = 36.00m, grupoId = 3 },

                // Grupo 4 - Bebidas
                new Produtos { id = 201, descricao = "Coca-Cola Lata", preco = 7.00m, grupoId = 4 },
                new Produtos { id = 202, descricao = "Cerveja Sapporo", preco = 25.00m, grupoId = 4 },
                new Produtos { id = 203, descricao = "Água Mineral sem Gás", preco = 5.00m, grupoId = 4 },
                new Produtos { id = 204, descricao = "Água Mineral com Gás", preco = 6.00m, grupoId = 4 },
                new Produtos { id = 205, descricao = "Chá Verde Gelado", preco = 12.00m, grupoId = 4 },
                new Produtos { id = 206, descricao = "Suco Natural de Laranja", preco = 14.00m, grupoId = 4 },
                new Produtos { id = 207, descricao = "Suco Natural de Abacaxi", preco = 14.00m, grupoId = 4 },
                new Produtos { id = 208, descricao = "Refrigerante Guaraná Lata", preco = 7.00m, grupoId = 4 },
                new Produtos { id = 209, descricao = "Cerveja Heineken Long Neck", preco = 20.00m, grupoId = 4 },
                new Produtos { id = 210, descricao = "Saquê Tradicional", preco = 30.00m, grupoId = 4 },
            };
        }

    }
}