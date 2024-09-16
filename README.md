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
