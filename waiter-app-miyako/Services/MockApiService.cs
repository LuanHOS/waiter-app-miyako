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
                new Grupos { id = 2, descricao = "Bebidas" },
                new Grupos { id = 3, descricao = "Pratos Quentes" },
                new Grupos { id = 4, descricao = "Sushis" },
            };
        }

        // Retorna os produtos do cardápio
        public async Task<List<Produtos>> FetchProdutos()
        {
            await Delay(800);
            return new List<Produtos>
            {
                // Barcas
                new Produtos { id = 101, descricao = "Barca Miyako (2 Pessoas)", preco = 120.00m, grupoId = 1 },
                new Produtos { id = 102, descricao = "Barca Salmão (1 Pessoa)", preco = 75.00m, grupoId = 1 },
                // Bebidas
                new Produtos { id = 201, descricao = "Coca-Cola Lata", preco = 7.00m, grupoId = 2 },
                new Produtos { id = 202, descricao = "Cerveja Sapporo", preco = 25.00m, grupoId = 2 },
                // Pratos Quentes
                new Produtos { id = 301, descricao = "Yakisoba de Carne", preco = 45.00m, grupoId = 3 },
                new Produtos { id = 302, descricao = "Tempurá de Camarão", preco = 55.00m, grupoId = 3 },
                // Sushis
                new Produtos { id = 401, descricao = "Uramaki Filadélfia (8 un)", preco = 32.00m, grupoId = 4 },
                new Produtos { id = 402, descricao = "Sashimi de Atum (5 fatias)", preco = 28.00m, grupoId = 4 },
            };
        }

        // ... (fetchProdutos, fetchCategorias, etc. podem ser adicionados aqui depois)
    }
}