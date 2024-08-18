
# Arquitetura

![Arquitetura](Gateway.API/Resources/Imagens/DiagramaArquitetura.png)

# Passos para executar o projeto
### 1 - Subir imagens
1. Com o Docker instalado, acessar a raiz do repositório e executar o comando no terminal: <code>docker-compose up</code>.
    
2. Verificar se as imagens <code>rabbitmq</code>, <code>sqlserver</code> estão rodando e suas respectivas portas.

3. A senha de acesso ao banco de dados é definido no arquivo <code>.env</code>.

### 1 - Atualizar banco de dados (migrations)

1. No Visual Studio, definir o projeto <code>Yia.UI</code> como projeto de inicialização e realizar o build da solução.

2. Abrir o Package Manager Console e definir o projeto de destino como <code>4 - Infra\4.1 - Data\Yia.Infra.Data</code>.

4. No Package Manager Console, atualizar/criar o banco de dados com o comando <code>update-database</code>.

### 2 - Acesso ao RabbitMQ

1. O acesso ao RabbitMQ se dá pelo navegador na porta em que está disponível o serviço (por padrão <code>http://localhost:15672/</code>), com o usuário <code>guest</code> e senha <code>guest</code>.

### 3 - Executando o projeto
1. Com todos os demais passos anteriores realizados, para executar a solução é necessário apenas definir os projetos de 
<code>Gateway.API</code>, <code>Identidade.API</code>, <code>Lancamento.API</code>, <code>Consolidacao.API</code> como múltiplos projetos de inicialização.