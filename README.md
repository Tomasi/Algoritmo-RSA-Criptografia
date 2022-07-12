# Documentação criptografia com RSA

Católica de Santa Catarina

Alunos: Igor Tomasi e Khauê Souza

## Introdução

A criptografia estuda os métodos de codificar uma mensagem de tal forma que apenas o destinário consiga decodificá-la, gerando assim uma segurança na troca de mensagens (Citação). O presente projeto, busca implementar e demonstrar a utilização prática da Criptografia com RSA, sendo uma técnica que utiliza a teorica de números para codificação e decodificação de mensagens.

Utiliza-se chaves assimétricas, ou seja, existem duas chaves distintas, uma pública e privada. Sua lógica de uso consiste basicamente, em deixar uma chave pública disponível para qualquer pessoa, ela é utilizada para codificar uma mensagem, em outra ponta teremos uma pessoa que estará em posse da chave privada, única. Nessa ponta, o usuário poderá, através da chave privada desencriptar o arquivo, um conceito amplo de como funciona essa ténica.

## Geração do conjunto de chaves

O algoritmo "GetKeys.java" do repositório, gera a chave pública e a chave privada. Sendo necessário, apenas passar para a aplicação, através da interação via console aplication um par de números primos grandes.

### Passo a Passo para chegar nas chaves 
Escolha de forma aleatória dois números primos grandes p e q
Calcule n = pq
Calcule a função totiente de Euler em: n: T(n) = (p-1)(q-1)
Escolha um inteiro e tal que 1 < e < T(n), de forma que e e T(n) sejam primos entre si
Calcule d de forma que d = e^-1 mod (T(n))

A chave pública é o par {e, n}, a chave privada {d, n}.

## Planejamento e Operação

Os projetos foram estruturados da seguinte forma, um método main que receberá como argumento três caminhos de arquivos, sendo NECESSÁRIO que a ordem de parâmetros seja respeitada, caso contrário o algoritimo não funcionará corretamente. 

O arquivo que contém as chaves, devem conter apenas os números das chaves, com três linhas e a ordem de parâmetros deve ser: 
1 - Módulo 
2 - Chave Privada 
3 - Chave Pública

![image](https://user-images.githubusercontent.com/61890715/178611061-18447932-9c62-4b90-84bd-ad61fc7774b7.png)

Após receber como parâmetro os argumentos, é realizado uma leitura no arquivo com as chaves assimétricas, buscando o módulo e a chave privada. Trabalharemos com o tipo BigInteger em C#, assim, teremos que converter o modulo e a key para sua representação em BigInteger, feito isso é realizado a leitura de todo o contúdo do arquivo a encriptar e codificamos em Base 64 UTF8.

Precisamos "quebrar" o resultado codificado em algumas partes, primeiro deve-se saber qual o tamanho da string, para cálcula-la foi utilizado um exemplo de TextChunk que utiliza como parâmetro o módulo em BigInteger para buscar o tamanho do bloco. 
O último passo é aplicar o split do texto em base 64 com o tamanho encontrado do bloco e realizar o cálculo para encriptar o conteúdo, gravando em um arquivo de saída.

Para descriptografar utilizamos agora, ao invés da chave private a chave pública. Realizamos a leitura do arquivo criptografado, e para cada linha encontrada é feito o cálculo com a chave pública e em seguida o decode do bloco, concatenando os resultados chegaremos ao conteúdo original.

## Problemas Encontrados no Desenvolvimento

Não foram encontrados muitos problemas, uma vez que entendido o conceito e o objetivo, a estruturação do código mostrou-se sem muitos problemas.

## Técnologias

Foi utilizada para desenvolvimento a linguagem C#, por apresentar fácil interpretação.
