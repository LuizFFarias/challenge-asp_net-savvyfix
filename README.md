# SavvyFix - Precificação dinâmica

## Lista dos integrantes:
#### 1. Douglas Magalhães de Araujo - 552008

#### 2. Erik Yuuzo Kobayachi Yamada - 98027

#### 2. Gustavo Argüello Bertacci - 551304

#### 3. Luiz Fillipe Farias - 99519

#### 4. Rafaella Monique do Carmo Bastos - 552425

<br>

## Arquitetura do sistema
![Arquitetura_Dotnet](https://github.com/user-attachments/assets/abef2011-e2b8-4e96-bb5a-8b84216a8dce)

<p>A arquitetura utilizada segue uma abordagem monolítica, ou seja, todas as funcionalidades desenvolvidas estão em interconectados e dependem uns dos outros.</p> 

<p>Como a Savvyfix é um projeto pequeno e com mais simplicidade, a abordagem monolítica promove uma maior eficiência no desenvolvimento inicial por conta dos seguintes pontos:</p>

<p>&nbsp;&nbsp;&nbsp;&nbsp;1. O serviço de aplicativo na Azure com a aplicação de c# é centralizado com um único serviço de aplicativo que contém várias responsabilidades, como Spring Boot e REST. Isso é útil para trazer mais facilidade no gerenciamento e integração, diminuindo riscos de falhas.</p>

<p>&nbsp;&nbsp;&nbsp;&nbsp;2. Com a utilização do banco de dados Oracle é centralizado para todas as aplicações, como Kotlin e Azure, a solução se torna mais acoplada. As vantagens dessa utilização é a diminuição das inconsistências de dados, fator importante para a precificação dinâmica.</p>

<p>&nbsp;&nbsp;&nbsp;&nbsp;3. As funcionalidades desenvolvidas não possuem divisão de responsabilidade entre os serviços, pois as operações convergem para o banco de dados e o serviço de aplicativo. Como o projeto é pequeno, isso é importante para diminuir a sobrecarga de comunicação entre muitos sistemas via APIs.</p>

<p>&nbsp;&nbsp;&nbsp;&nbsp;4. Caso haja uma alteração em uma aplicação, todas as outras aplicações são impactadas e devem ser recompiladas por estarem conectadas e dividirem a mesma modelagem. Essa abordagem garante a consistência entre todos os sistemas, pois, ao ser realizado uma alteração, será necessário conferir em todas as outras aplicações se não há mais codificações inconsistentes.</p>

<br>

## Design Patterns - Service Layer
<p>A Service Layer (Camada de Serviço) é um padrão arquitetural que separa a lógica de negócios da lógica de apresentação e de persistência de dados. Ela centraliza a lógica de operações do sistema em classes de serviços, facilitando a manutenção e escalabilidade do código.</p>
<p>Em nosso projeto foi criada uma camada Service para o tratamento de precificação e manipulação dos dados antes de serem registrados em nosso banco de dados, garantindo assim uma organização do código e
segurança no tratamento das informações passadas pelo usuário.</p>

## Testes Unitários com XUnit

<p>Foi utilizado um padrão de testes de todos os endpoints, testes positivos e negativos e para explicar a funcionalidade dos testes utilizamos os testes de Atividades
  
### Autenticação:

Em vários testes, o método GerarTokenAdminJwtDeTeste do TokenService é utilizado para gerar um token de autenticação simulado. Esse token é adicionado ao cabeçalho de autorização para permitir acesso a endpoints protegidos.
Testes de Listagem e Obtenção de Atividades:

GetAtividades_RetornaListaDeAtividades: verifica se a API retorna uma lista de atividades quando o usuário está autenticado.
GetAtividadesSemAutorizacao_RetornaUnauthorized: garante que a API retorna Unauthorized se a chamada for feita sem autenticação.
GetAtividadeById_RetornaAtividade_SeAtividadeExiste: verifica se uma atividade específica é retornada ao fornecer um ID válido.
GetAtividadeById_RetornaStatus404_SeAtividadeNaoExiste: valida que a resposta é NotFound quando uma atividade inexistente é solicitada.

### Testes de Criação de Atividades:

CriaAtividade_RetornaAtividadeCadastrada: cria uma nova atividade e verifica se a resposta da API inclui a atividade recém-criada com os dados corretos.
CriaAtividade_RetornaErro_SeVariosAtributosInvalidos: usa Theory e InlineData para testar vários cenários com dados inválidos, garantindo que a API retorne BadRequest para entradas incorretas.

### Testes de Atualização:

AtualizaCliente_RetornaClienteAtualizado_SeClienteExiste e AtualizaAtividade_RetornaAtividadeAtualizada_SeAtividadeExiste: verificam se as atualizações de cliente e atividade retornam os dados modificados corretamente, quando a entidade existe.

### Testes de Exclusão de Atividades:

DeletaAtividade_RetornaNoContent_SeAtividadeExiste: garante que a exclusão de uma atividade válida retorna NoContent, indicando sucesso.
DeletaAtividade_RetornaStatus404_SeAtividadeNaoExiste: assegura que tentar deletar uma atividade inexistente resulta em NotFound.
DeletaAtividade_RetornaForbidden_SeNaoForAdmin: verifica se um usuário sem permissões de administrador recebe Forbidden ao tentar excluir uma atividade.</p>

## Práticas de CleanCode

<p>
O projeto foi organizado em pacotes de solução distintos, facilitando a manutenção, testabilidade e escalabilidade da aplicação.

Estrutura do Projeto
O projeto é dividido em quatro pacotes principais, cada um com uma responsabilidade específica:

1. SavvyfixAspNet.Test
Este pacote é dedicado a testes unitários e de integração, garantindo que todas as funcionalidades do sistema sejam validadas sem interferir na lógica de produção. A separação dos testes permite uma abordagem clara e organizada, facilitando a execução e a manutenção dos mesmos.

2. SavvyfixAspNet.ML
Este pacote contém a lógica relacionada ao aprendizado de máquina. Ao isolá-lo, garantimos que a complexidade dos modelos de machine learning não afete a lógica de negócio do aplicativo. Isso também permite que futuras melhorias e alterações na lógica de ML sejam realizadas de forma segura.

3. SavvyfixAspNet.Api
A camada de apresentação, responsável pelos endpoints da API, serviços e modelos. A separação desta camada assegura que as alterações na interface da API não impactem a lógica de domínio ou de dados, proporcionando uma estrutura clara e modular.

4. SavvyfixAspNet.Data e SavvyfixAspNet.Domain
SavvyfixAspNet.Data: Contém o contexto do banco de dados e as migrações, isolando a lógica de acesso a dados.
SavvyfixAspNet.Domain: Abriga as entidades do domínio, promovendo a clareza e a organização dos modelos utilizados na aplicação.

Com essas técnicas garantimos na nossa aplicação Legebilidade e a Testabilidade facilitando a compreensão e novas atualizações. 

![image](https://github.com/user-attachments/assets/59d61d0f-df62-4451-80c6-746dfadc6313)
</p>

## IA Generativa utilizada

<p>
  O SavvyFix utiliza machine learning para oferecer uma precificação dinâmica e personalizada com base em diversos critérios. Esse processo analisa dados de atividades e aplica percentuais de ajuste, gerando um preço final mais preciso para cada cliente que realiza compras na API.

Estrutura do Dataset
O projeto utiliza um arquivo dataset.csv contendo informações detalhadas das atividades, com atributos como localização, horário, clima, frequência de procura e demanda. Esses dados são combinados para determinar um percentual que ajusta o valor final das atividades, criando uma experiência personalizada para o cliente.

Abaixo estão alguns dos critérios utilizados:

Localização: Aplica percentuais que variam conforme o estado. Por exemplo, RS recebe um ajuste de 3%, enquanto regiões menos procuradas, como AC, têm ajustes negativos de até -3%.
Horário: As faixas horárias também afetam o preço. Horários de pico, como 12h-12h59, têm um aumento de 3%, enquanto horários menos movimentados, como 03h-03h59, têm uma redução de 3%.
Clima: A faixa de temperatura ajusta o preço, de modo que temperaturas mais extremas, como 1º a 3º e acima de 40º, possuem ajustes positivos.
Procura: Ajusta o preço com base na frequência de demanda.
Demanda: Aplica um ajuste específico conforme a demanda seja alta, média ou baixa.
Pipeline de Treinamento
O pipeline de treinamento no SavvyFix utiliza o contexto de machine learning para combinar as porcentagens ajustadas e, em seguida, treina um modelo de regressão para prever o preço final das atividades. Esse modelo é salvo em um arquivo .zip, permitindo que seja carregado e reutilizado para fazer previsões sem necessidade de retreinamento.

Passos do Pipeline:
Concatenar Atributos: Usamos o método Concatenate para unir todas as porcentagens (PorcentagemLocalizacao, PorcentagemHorario, PorcentagemClima, PorcentagemProcura, PorcentagemDemanda) em uma única coluna de recursos chamada Features.
Treinador de Regressão: Em seguida, aplicamos um treinador de regressão Sdca para ajustar o modelo com base no preço final (PrecoFinal), considerando até 100 iterações para otimizar os resultados.
Salvar o Modelo Treinado: O modelo é salvo em um arquivo modelo.zip, permitindo seu carregamento para realizar previsões sem precisar ser treinado novamente.
</p>

## Instruções para rodar a API

### Realizar o clone do projeto 

#### <p>O primeiro passoa para testar a apliação é realizar o git clone do nosso projeto através do linK: https://github.com/LuizFFarias/challenge-asp_net-savvyfix.git</p>


<p>1. Abra um terminal e digite: git clone https://github.com/LuizFFarias/challenge-asp_net-savvyfix.git</p>
<p>2. Abra o projeto em uma IDE destinada para C#/.NET como Rider ou Visual Studio.</p>
<p>3. Inicie o projeto SavvyfixAspNet.Api como http.</p>
<p>4. Após iniciar, entre no link abaixo para testar a aplicação através do Swagger UI: </p>
http://localhost:5255/swagger/index.html

<br>

## Teste da API

![image](https://github.com/user-attachments/assets/c1cb9de4-c21f-4fa5-8272-29cd0a151142)


<p>Essa é a página inicial da documentação do projeto, para realizar a autenticação pelo Token gerado. </p>

![image](https://github.com/user-attachments/assets/814d0cdb-c036-4f4d-be91-4cf8cd4afee5)


<p>Depois copiar o token gerado e autenticar. </p>

![image](https://github.com/user-attachments/assets/ee43cd99-0671-4e32-a930-8b64c2b473be)

<p>Agora podemos utilizar a API.</p>

![image](https://github.com/user-attachments/assets/c134f568-970e-448b-b837-18ad0dca5e6a)

<p>Resposta do get</p>

![image](https://github.com/user-attachments/assets/0c5c3b60-33b4-42f9-a200-d29c27f86aec)


<p>Para fazer uma inserção, vá em POST e altere o JSON para inserir as informações que deseja cadastrar no banco de dados.</p>

![image](https://github.com/user-attachments/assets/65b06dc9-3566-4bb7-8d3a-7cd67292f8d5)


<p>Para editar, vá em PUT e informe o id e os dados que serão alterados. Lembrando que para editar e deletar produto o role do usuário deve ser ROLE_ADMIN</p>

![image](https://github.com/user-attachments/assets/43850f32-e293-42d3-bdf7-91e40e05cf6b)

<p>Para deletar, vá em DELETE e informe o id.</p>

![image](https://github.com/user-attachments/assets/19484a2e-ac24-4561-b351-c7722af69a01)








