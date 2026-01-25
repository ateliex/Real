#language: pt-br

Funcionalidade: Criação Categoria

Cenário: Sucesso ao criar uma categoria
	Dado que o usuário está criando uma nova categoria
	Quando o usuário preencher o nome da categoria com 'Lazer'
	E o usuário salvar a categoria
	Então o sistema deverá criar a categoria como esperado