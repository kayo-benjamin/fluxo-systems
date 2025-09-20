# 🚀 Fluxo - Sistema de Gestão de Produtos

![Badge](https://img.shields.io/badge/language-C%23-blue)
![Badge](https://img.shields.io/badge/database-PostgreSQL-blue)
![Badge](https://img.shields.io/badge/licen%C3%A7a-MIT-green)

<p align="center">
  </p>

Um sistema desktop simples e robusto para gerenciamento de produtos (CRUD), desenvolvido como um projeto de estudo para aplicar conceitos de arquitetura em camadas, acesso a dados e boas práticas de desenvolvimento em C#.

---

## ✨ Features

* **Cadastro de Produtos:** Adicione novos produtos ao estoque.
* **Visualização de Produtos:** Liste todos os produtos cadastrados em uma grade de fácil visualização.
* **Edição de Produtos:** Selecione um produto na lista e atualize suas informações.
* **Exclusão de Produtos:** Remova produtos do catálogo.
* **Interface Intuitiva:** Uma tela única para realizar todas as operações de forma rápida e eficiente.

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C#
* **Plataforma:** .NET Framework
* **Interface Gráfica:** Windows Forms
* **Banco de Dados:** PostgreSQL
* **Driver de Conexão:** Npgsql
* **Configuração:** Leitura de `appsettings.json` para a Connection String.

---

## 🏗️ Arquitetura

O projeto foi estruturado utilizando uma **Arquitetura em Camadas** para garantir a separação de responsabilidades, facilitando a manutenção e a escalabilidade do código.

```
/ 📂 Fluxo
|
|-- 📂 View/
|   |-- frmProdutos.cs      # A tela que o usuário vê e interage (Apresentação)
|
|-- 📂 Controller/
|   |-- ProdutoController.cs  # O maestro que conecta a View com as regras de negócio
|
|-- 📂 Model/
|   |-- Produto.cs          # A definição do que é um Produto (Entidade)
|   |-- ProdutoDao.cs       # A classe que sabe conversar com o banco de dados (Acesso a Dados)
|   |-- AppSettings.cs      # Classe auxiliar para ler as configurações
|
|-- 📂 Interface/
|   |-- IProdutoDao.cs      # O "contrato" que define as regras do acesso a dados
|
|-- 📄 Program.cs              # Ponto de entrada da aplicação
|-- 📄 appsettings.json         # Arquivo de configuração (Connection String)
```
Essa estrutura desacopla a interface do usuário da lógica de acesso ao banco de dados, tornando o sistema mais organizado e profissional.

---

## 🚀 Como Executar o Projeto

Siga os passos abaixo para rodar o projeto em sua máquina local.

### Pré-requisitos

* **Visual Studio** (2019 ou superior) com a carga de trabalho ".NET Desktop Development".
* Uma instância do **PostgreSQL** rodando localmente ou em um servidor.

### Passos

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/kayo-benjamin/fluxo-systems.git](https://github.com/kayo-benjamin/fluxo-systems.git)
    ```

2.  **Abra o projeto:**
    * Navegue até a pasta do projeto e abra o arquivo `Fluxo.sln` com o Visual Studio.

3.  **Crie o Banco de Dados:**
    * Execute o script SQL abaixo no seu PostgreSQL para criar a tabela `produto`:
      ```sql
      CREATE TABLE produto (
        id_produto SERIAL PRIMARY KEY,
        nome VARCHAR(45) NOT NULL,
        preco DECIMAL(10,2) NOT NULL,
        quantidade INT DEFAULT 0
      );
      ```

4.  **Configure a Conexão:**
    * Abra o arquivo `appsettings.json`.
    * Altere os valores de `Host`, `Database`, `Username` e `Password` para corresponderem às configurações do seu banco de dados PostgreSQL.
      ```json
      {
        "ConnectionStrings": {
          "DefaultConnection": "Host=127.0.0.1;Database=fluxo_db;Username=seu_usuario;Password=sua_senha;"
        }
      }
      ```
    * **Importante:** No Visual Studio, clique com o botão direito no arquivo `appsettings.json` > `Propriedades` e mude **"Copiar para Diretório de Saída"** para **"Copiar se for mais novo"**.

5.  **Rode o Projeto:**
    * Pressione `F5` ou clique no botão "Start" do Visual Studio para compilar e executar a aplicação.

---

## 🖼️ Screenshots

*Aqui você pode adicionar imagens da sua aplicação em funcionamento!*

![Tela Principal do Fluxo]([./screenshots/tela-principal.png](https://github.com/kayo-benjamin/fluxo-systems/tree/main/Fluxo/screenshots))

---

## 👨‍💻 Autor

Feito com ❤️ por **[Kayo Benjamin]**.

[![LinkedIn](https://img.shields.io/badge/linkedin-%230077B5.svg?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/kayo-vinicius-85467522b/)

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE.md) para mais detalhes.
