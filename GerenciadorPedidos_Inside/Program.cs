using System;
using System.Collections.Generic; //Utilizado para criar a lista
using System.Linq; //Utilizado para filtrar a lista

namespace GerenciadorPedidos
{
    // Classe que representa um Produto
    public class Produto
    {
        public string Nome { get; set; } // Nome do produto
        public string Descricao { get; set; } // Descrição do produto
        public decimal Dinheiro { get; set; } // Preço do produto
        public Status StatusDoProduto { get; set; } // Status do produto: Aberto ou Fechado

        // Enum para representar o status do produto
        public enum Status { Aberto, Fechado }
    }

    // Classe que representa um Pedido
    public class Pedido
    {
        public string NomePedido { get; set; } // Nome do pedido
        public List<Produto> Produtos { get; set; } // Lista de produtos associados ao pedido
        public Produto.Status StatusDoPedido { get; set; } // Status do pedido: Aberto ou Fechado
    }

    // Classe principal que contém a lógica do programa
    public class Program
    {
        // Lista global para armazenar todos os pedidos
        public static List<Pedido> listaDePedidos = new List<Pedido>();

        static void Main(string[] args)
        {
            int opcoes;

            while (true) // Loop principal do menu
            {
                Console.Clear(); // Limpa a tela para exibir o menu novamente
                Console.WriteLine("===============================================");
                Console.WriteLine("        BEM-VINDO À SUA LISTA DE PEDIDOS      ");
                Console.WriteLine("===============================================\n");
                Console.WriteLine("Escolha uma das opções abaixo para continuar:");
                Console.WriteLine("1 - Iniciar Novo Pedido");
                Console.WriteLine("2 - Adicionar Produto a um Pedido");
                Console.WriteLine("3 - Remover Produto de um Pedido");
                Console.WriteLine("4 - Fechar Pedido");
                Console.WriteLine("5 - Reabrir Pedido Fechado");
                Console.WriteLine("6 - Listar pedidos");
                Console.WriteLine("7 - Sair da Lista de Pedidos");

                // Verifica se a opção é válida
                if (!int.TryParse(Console.ReadLine(), out opcoes) || opcoes < 1 || opcoes > 7)
                {
                    Console.WriteLine("Opção inválida. Tente novamente.\n");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    continue; // Volta para o início do loop
                }

                // Chama a função correspondente à escolha do usuário
                switch (opcoes)
                {
                    case 1:
                        IniciarNovoPedido();
                        break;
                    case 2:
                        if (listaDePedidos.Count == 0) // Verifica se há pedidos para adicionar produtos
                        {
                            Console.WriteLine("Nenhum pedido iniciado. Por favor, inicie um novo pedido antes de adicionar produtos.\n");
                        }
                        else
                        {
                            AdicionarProdutos();
                        }
                        break;
                    case 3:
                        if (listaDePedidos.Count == 0) // Verifica se há pedidos para remover produtos
                        {
                            Console.WriteLine("Nenhum pedido iniciado. Por favor, inicie um novo pedido antes de remover produtos.\n");
                        }
                        else
                        {
                            RemoverProdutos();
                        }
                        break;
                    case 4:
                        if (listaDePedidos.Count == 0) // Verifica se há pedidos para fechar
                        {
                            Console.WriteLine("Nenhum pedido iniciado. Por favor, inicie um novo pedido antes de fechar um pedido.\n");
                        }
                        else
                        {
                            FecharPedido();
                        }
                        break;
                    case 5:
                        if (listaDePedidos.Count == 0) // Verifica se há pedidos para reabrir
                        {
                            Console.WriteLine("Nenhum pedido iniciado. Por favor, inicie um novo pedido antes de reabrir um pedido.\n");
                        }
                        else
                        {
                            ReabrirPedido();
                        }
                        break;
                    case 6:
                        if (listaDePedidos.Count == 0) // Verifica se há pedidos para listar
                        {
                            Console.WriteLine("Nenhum pedido iniciado. Por favor, inicie um novo pedido antes de listar pedidos.\n");
                        }
                        else
                        {
                            ListarPedidos();
                        }
                        break;
                    case 7:
                        Sair(); // Encerra o programa
                        return;
                }

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        // Função para iniciar um novo pedido
        public static void IniciarNovoPedido()
        {
            Console.WriteLine("Digite o nome do novo pedido:\n");
            string nomeNovoPedido = Console.ReadLine();

            // Verifica se já existe um pedido com o mesmo nome
            if (listaDePedidos.Any(p => p.NomePedido == nomeNovoPedido))
            {
                Console.WriteLine("Já existe um pedido com esse nome. Tente um nome diferente.\n");
                return;
            }

            // Cria um novo pedido e adiciona à lista
            var novoPedido = new Pedido
            {
                NomePedido = nomeNovoPedido,
                Produtos = new List<Produto>(),
                StatusDoPedido = Produto.Status.Aberto
            };

            listaDePedidos.Add(novoPedido); // Adiciona o novo pedido à lista
            Console.WriteLine("Novo pedido iniciado com sucesso.\n");
        }

        // Função para adicionar produtos a um pedido aberto
        public static void AdicionarProdutos()
        {
            var pedidosAbertos = listaDePedidos.Where(p => p.StatusDoPedido == Produto.Status.Aberto).ToList();

            // Verifica se há pedidos abertos
            if (pedidosAbertos.Count == 0)
            {
                Console.WriteLine("Não há nenhum pedido aberto. Por favor, inicie um novo pedido.\n");
                return;
            }

            Console.WriteLine("Escolha um pedido ao qual deseja adicionar o produto:\n");
            for (int i = 0; i < pedidosAbertos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Pedido: {pedidosAbertos[i].NomePedido}\n");
            }

            int escolha;
            while (true)
            {
                // Valida a entrada do usuário
                if (int.TryParse(Console.ReadLine(), out escolha) && escolha > 0 && escolha <= pedidosAbertos.Count)
                {
                    break; // Sai do loop se a entrada for válida
                }

                Console.WriteLine("Seleção inválida. Tente novamente.\n");
            }

            var pedidoSelecionado = pedidosAbertos[escolha - 1];

            Console.WriteLine("Digite o nome do produto:\n");
            string nomeProduto = Console.ReadLine();

            Console.WriteLine("Digite a descrição do produto (até 50 caracteres):\n");
            string descricaoProduto;
            while (true)
            {
                descricaoProduto = Console.ReadLine();
                if (descricaoProduto.Length <= 50)
                    break; // Sai do loop se a descrição tiver até 50 caracteres

                Console.WriteLine("A descrição precisa ter até 50 caracteres. Tente novamente:\n");
            }

            decimal valorProduto;
            Console.WriteLine("Digite o preço do produto:\n");
            while (!decimal.TryParse(Console.ReadLine(), out valorProduto) || valorProduto <= 0)
            {
                Console.WriteLine("Valor inválido. Digite um preço válido:\n");
            }

            // Cria um novo produto e o adiciona ao pedido selecionado
            var novoProduto = new Produto
            {
                Nome = nomeProduto,
                Descricao = descricaoProduto,
                Dinheiro = valorProduto,
                StatusDoProduto = Produto.Status.Aberto
            };

            pedidoSelecionado.Produtos.Add(novoProduto); // Adiciona o produto ao pedido
            Console.WriteLine("Produto adicionado ao pedido com sucesso.\n");
        }

        // Função para remover produtos de um pedido
        public static void RemoverProdutos()
        {
            Console.WriteLine("Escolha de qual pedido deseja remover um produto:\n");
            for (int i = 0; i < listaDePedidos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Pedido: {listaDePedidos[i].NomePedido}, Status: {listaDePedidos[i].StatusDoPedido}");
            }

            int escolha;
            while (true)
            {
                // Valida a entrada do usuário
                if (int.TryParse(Console.ReadLine(), out escolha) && escolha > 0 && escolha <= listaDePedidos.Count)
                {
                    break; // Sai do loop se a entrada for válida
                }

                Console.WriteLine("Seleção inválida. Tente novamente.\n");
            }

            var pedidoSelecionado = listaDePedidos[escolha - 1];

            // Verifica se o pedido está fechado
            if (pedidoSelecionado.StatusDoPedido == Produto.Status.Fechado)
            {
                Console.WriteLine("Não é possível remover produtos de um pedido fechado.\n");
                return;
            }

            // Verifica se o pedido contém produtos
            if (pedidoSelecionado.Produtos.Count == 0)
            {
                Console.WriteLine("Não há produtos para remover neste pedido.\n");
                return;
            }

            Console.WriteLine("Produtos no pedido:\n");
            foreach (var produto in pedidoSelecionado.Produtos)
            {
                Console.WriteLine($"Nome: {produto.Nome}, Descrição: {produto.Descricao}, Valor: R$ {produto.Dinheiro}, Status: {produto.StatusDoProduto}\n");
            }

            Console.WriteLine("Digite o nome do produto que deseja remover:\n");
            string nomeProdutoRemover = Console.ReadLine();

            // Encontra o produto pelo nome
            var produtoRemover = pedidoSelecionado.Produtos.FirstOrDefault(p => p.Nome.Equals(nomeProdutoRemover, StringComparison.OrdinalIgnoreCase));

            // Verifica se o produto existe no pedido
            if (produtoRemover == null)
            {
                Console.WriteLine("Produto não encontrado no pedido.\n");
                return;
            }

            // Remove o produto do pedido
            pedidoSelecionado.Produtos.Remove(produtoRemover);
            Console.WriteLine("Produto removido com sucesso.\n");
        }

        // Função para fechar um pedido
        public static void FecharPedido()
        {
            // Lista pedidos abertos para seleção
            var pedidosAbertos = listaDePedidos.Where(p => p.StatusDoPedido == Produto.Status.Aberto).ToList();
            if (pedidosAbertos.Count == 0)
            {
                Console.WriteLine("Não há pedidos abertos para fechar.\n");
                return;
            }

            Console.WriteLine("Escolha qual pedido deseja fechar:\n");
            for (int i = 0; i < pedidosAbertos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Pedido: {pedidosAbertos[i].NomePedido}\n");
            }

            int escolha;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out escolha) && escolha > 0 && escolha <= pedidosAbertos.Count)
                {
                    break; // Valida a escolha do usuário
                }

                Console.WriteLine("Seleção inválida. Tente novamente.\n");
            }

            var pedidoSelecionado = pedidosAbertos[escolha - 1];
            pedidoSelecionado.StatusDoPedido = Produto.Status.Fechado; // Define o status como Fechado
            Console.WriteLine("Pedido fechado com sucesso.\n");
        }

        // Função para reabrir um pedido fechado
        public static void ReabrirPedido()
        {
            var pedidosFechados = listaDePedidos.Where(p => p.StatusDoPedido == Produto.Status.Fechado).ToList();
            if (pedidosFechados.Count == 0)
            {
                Console.WriteLine("Não há pedidos fechados para reabrir.\n");
                return;
            }

            Console.WriteLine("Escolha qual pedido deseja reabrir:\n");
            for (int i = 0; i < pedidosFechados.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Pedido: {pedidosFechados[i].NomePedido}\n");
            }

            int escolha;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out escolha) && escolha > 0 && escolha <= pedidosFechados.Count)
                {
                    break;
                }

                Console.WriteLine("Seleção inválida. Tente novamente.\n");
            }

            var pedidoSelecionado = pedidosFechados[escolha - 1];
            pedidoSelecionado.StatusDoPedido = Produto.Status.Aberto; // Define o status como Aberto
            Console.WriteLine("Pedido reaberto com sucesso.\n");
        }

        // Função para listar todos os pedidos
        public static void ListarPedidos()
        {
            Console.WriteLine("Pedidos:\n");

            // Itera sobre a lista de pedidos e imprime suas informações
            foreach (var pedido in listaDePedidos)
            {
                Console.WriteLine($"Pedido: {pedido.NomePedido}, Status: {pedido.StatusDoPedido}");
                if (pedido.Produtos.Count == 0)
                {
                    Console.WriteLine("  Este pedido não contém produtos.\n");
                }
                else
                {
                    Console.WriteLine("  Produtos:\n");
                    foreach (var produto in pedido.Produtos)
                    {
                        Console.WriteLine($"  Nome: {produto.Nome}, Descrição: {produto.Descricao}, Valor: R$ {produto.Dinheiro}, Status: {produto.StatusDoProduto}");
                    }
                    Console.WriteLine();
                }
            }
        }

        // Função para sair do programa
        public static void Sair()
        {
            Console.WriteLine("Obrigado por usar o sistema de pedidos. Até logo!\n");
        }
    }
}
