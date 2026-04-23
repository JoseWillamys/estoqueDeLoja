var estoque = new Dictionary<string, int>();

void ExibirMenu()
{
    Console.Clear();
    Console.WriteLine("Menu do estoque:\n");
    Console.WriteLine("1 - Registrar produto");
    Console.WriteLine("2 - Alterar produto");
    Console.WriteLine("3 - Exibir produtos registrados");
    Console.WriteLine("4 - Pesquisar produto");
    Console.WriteLine("5 - Sair");
    Console.Write("\nDigite o número da opção desejada: ");
    int opcaoEscolhida = int.Parse(Console.ReadLine()!);
    ExibirOpcaoEscolhida(opcaoEscolhida);
}

void ExibirOpcaoEscolhida(int opcao)
{
    switch (opcao)
    {
        case 1:
            RegistrarProdutos();
            break;
        case 2:
            MenuDeAlteracoes();
            break;
        case 3:
            ExibirProdutosRegistrados();
            break;
        case 4:
            PesquisarProduto();
            break;
        case 5:
            Console.WriteLine("Programa finalizado!");
            break;
        default:
            Console.WriteLine("Opção inválida.");
            Thread.Sleep(2000);
            ExibirMenu();
            break;
    }
}

void RegistrarProdutos()
{
    Console.Clear();
    Console.WriteLine("Registro de produtos\n");
    string produto = ValidarInformacoes("Informe o nome do produto: ");
    int quantidadeDoProduto = ValidarConversao("\nInforme a quantidade do produto: ");
    estoque.Add(produto, quantidadeDoProduto);
    Console.WriteLine("\nO produto foi registrado com sucesso!");
    Thread.Sleep(2000);
    ExibirMenu();
}

// para prevenir string vazias ou com somente espaços
string ValidarInformacoes(string mensagem)
{
    string? entrada;

    do
    {
        Console.Write(mensagem);
        entrada = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(entrada))
        {
            Console.WriteLine("\nInformação inválida!");
        }
    }
    while (string.IsNullOrWhiteSpace(entrada));

    return entrada;
}

// para garantir que os dados númericos sejam válidos
int ValidarConversao (string mensagem)
{
    int numerico;
    string? entrada;

    do
    {
        Console.Write(mensagem);
        entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out numerico))
        {
            Console.WriteLine("\nInformação inválida!");
        }
    }
    while (!int.TryParse(entrada, out numerico));

    return numerico;
}

void ExibirProdutosRegistrados()
{
    Console.Clear();
    Console.WriteLine("Lista de Produtos do estoque\n");
    int contador = 1;
    foreach (string produto in estoque.Keys)
    {
        Console.WriteLine($"{contador}. {produto} - Quantidade = {estoque[produto]}");
    }

    Console.WriteLine("\nAperte qualquer tecla para voltar ao menu principal");
    Console.ReadKey();
    ExibirMenu();
}

void MenuDeAlteracoes()
{
    Console.Clear();
    Console.WriteLine("Atualizar informações de entrada/saída de produtos\n");
    string produto = ValidarInformacoes("Informe o nome do produdo: ");

    if (estoque.TryGetValue(produto, out int quantidade))
    {
        Console.WriteLine($"\n{produto} encontrado, quantidade: {quantidade}");
        Console.WriteLine("\nDeseja remover ou adicionar:");
        Console.WriteLine("\n1 - Remover");
        Console.WriteLine("2 - Adicionar");
        Console.Write("\nDigite o número da opção desejada: ");
        int opcao = int.Parse(Console.ReadLine()!);
        AcoesDeAlteracao(opcao, produto);
    }
    else
    {
        Console.WriteLine("\nO Produto informado não existe no estoque!");
    }

    Console.WriteLine("\nAperte qualquer tecla para voltar ao menu principal");
    Console.ReadKey();
    ExibirMenu();
}

void AcoesDeAlteracao(int opcao, string produto)
{
    switch (opcao)
    {
        case 1:
            RemoverProduto(produto);
            break;
        case 2:
            AdicionarProduto(produto);
            break;
        default:
            Console.WriteLine("Opção inválida.");
            Thread.Sleep(2000);
            ExibirMenu();
            break;
    }
}

void RemoverProduto(string produto)
{
    int valorInformado = ValidarConversao("Qual a quantidade do produto que você deseja remover: ");

    if (estoque[produto] > valorInformado)
    {
        estoque[produto] -= valorInformado;
        Console.WriteLine("Remoção feita com sucesso!");
    } else
    {
        Console.WriteLine("A quantidade para remoção é maior que o estoque disponível");
    }
}

void AdicionarProduto(string produto)
{
    int valorInformado = ValidarConversao("Qual a quantidade do produto que você deseja adicionar: ");
    estoque[produto] += valorInformado;
    Console.WriteLine("A quantidade do produto em estoque foi atualizada!");
}

void PesquisarProduto()
{
    char continuar;

    do
    {
        Console.Clear();
        string produto = ValidarInformacoes("Informe o nome do produdo: ");

        if (estoque.TryGetValue(produto, out int quantidade))
        {
            Console.WriteLine($"\n{produto} encontrado, quantidade: {quantidade}");
            continuar = char.Parse(ValidarInformacoes("\nDeseja fazer procurar outro produto s/n ?: "));
        }
        else
        {
            Console.WriteLine("\nO Produto informado não existe no estoque!");
            continuar = char.Parse(ValidarInformacoes("\nDeseja fazer procurar outro produto s/n ?: "));
        }
    } while (continuar == 's');

    Console.WriteLine("\nAperte qualquer tecla para voltar ao menu principal");
    Console.ReadKey();
    ExibirMenu();
}

ExibirMenu();