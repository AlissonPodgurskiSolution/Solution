
# Arquitetura

![Arquitetura](Gateway.API/Resources/Imagens/DiagramaArquitetura.png)

# Passos para executar o projeto
### 1 - Subir imagens
1. Com o Docker instalado, acessar a raiz do repositório e executar o comando no terminal: <code>docker-compose up</code>.
    
2. Verificar se as imagens <code>rabbitmq</code>, <code>sqlserver</code> estão rodando e suas respectivas portas.

3. A senha de acesso ao banco de dados é definido no arquivo <code>.env</code>.

4. Para levantar as instancias do rabbitmq e sqlserver para acessar a pasta docker e rodar o comando docker-compose up.

### 1 - Atualizar banco de dados (migrations)

1. Ao inicializar cada um dos micro serviços os mesmos devem rodar as migrations automaticamente criando as respectivas bases de dados:

A aplicação está utilizando o (localdb)\\mssqllocaldb, banco de dados local da microsoft.

Caso não ocorra a forma mencionada acima será necessário ir projeto a a projeto e rodar o comando <code>update-database</code> dentro do package manager console.

### 2 - Acesso ao RabbitMQ

1. O acesso ao RabbitMQ se dá pelo navegador na porta em que está disponível o serviço (por padrão <code>http://localhost:15672/</code>), com o usuário <code>guest</code> e senha <code>guest</code>.

### 3 - Executando o projeto
1. Com todos os demais passos anteriores realizados, para executar a solução é necessário apenas definir os projetos de 
<code>Gateway.API</code>, <code>Identidade.API</code>, <code>Lancamento.API</code>, <code>Consolidacao.API</code> como múltiplos projetos de inicialização.

Seguir o fluxo abaixo:

![Arquitetura](Gateway.API/Resources/Imagens/Fluxo.png)
