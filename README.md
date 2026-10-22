# Gestoque · Projeto de QA

Projeto de Quality Assurance aplicado a um sistema web de controle de estoque. O repositório reúne o sistema e todo o ciclo de testes: planejamento, casos de teste, registro de bugs, automação e testes de segurança.

## Sobre o sistema

O **Gestoque** é um sistema legado que desenvolvi em 2019 e modernizei para servir de objeto de estudo de QA. Ele permite:

- **Cadastros:** produtos, grupos, marcas, unidades de medida, fornecedores, locais de armazenamento, usuários e perfis
- **Operações:** entrada e saída de produtos, inventário e lançamento de perdas
- **Relatórios:** posição de estoque e ressuprimento (exportação em PDF)
- **Conta:** login com perfis de acesso, alteração e redefinição de senha

## Objetivo deste projeto

Demonstrar um ciclo completo de QA em um cenário realista de sistema legado:

1. Planejamento de testes e análise de riscos
2. Casos de teste com técnicas formais (partição de equivalência, valor limite, tabela de decisão)
3. Execução e registro de bugs com evidências
4. Automação de testes E2E e de endpoints HTTP
5. Testes de segurança
6. Correção dos bugs com testes de regressão

## Tecnologias

**Sistema**
- ASP.NET MVC 5.2 · .NET Framework 4.8
- Entity Framework 6.2 · Dapper · AutoMapper
- SQL Server LocalDB

**Testes**
- *A definir conforme o projeto evolui*

## Como executar

### Pré-requisitos

- Windows
- Visual Studio 2019 ou superior, com a carga de trabalho "Desenvolvimento para ASP.NET e Web"
- SQL Server Express LocalDB (instalado junto com o Visual Studio)

### 1. Criar o banco de dados

Na raiz do repositório, execute os scripts na ordem:

```
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\00-criar-banco.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -d Gestoque -i database\01-estrutura.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -d Gestoque -i database\02-dados-exemplo.sql
```

O script `00` recria o banco do zero, então pode ser usado para restaurar o ambiente de testes a qualquer momento.

### 2. Executar a aplicação

1. Abra `app/Gestoque.sln` no Visual Studio
2. Restaure os pacotes NuGet (botão direito na solução → *Restaurar Pacotes NuGet*)
3. Pressione **F5**

A aplicação abre na tela de login.

### Usuários de teste

| Login | Senha | Perfil |
|---|---|---|
| `gerente` | `Gerente@123` | Gerente |
| `administrativo` | `Admin@123` | Administrativo |
| `operador` | `Operador@123` | Operador |

> Credenciais fictícias, válidas apenas para o ambiente local.

### E-mails

O sistema não envia e-mails reais. As mensagens (por exemplo, a de redefinição de senha) são gravadas como arquivos `.eml` em `app/Gestoque/App_Data/emails/`.

## Documentação de QA

*Em construção.*

## Status

- [x] Baseline do sistema (`v1.0-original`)
- [x] README
- [ ] Plano de testes
- [ ] Casos de teste
- [ ] Bug reports
- [ ] Automação E2E
- [ ] Testes de endpoints HTTP
- [ ] Testes de segurança
- [ ] Integração contínua (CI)
- [ ] Correções com testes de regressão
