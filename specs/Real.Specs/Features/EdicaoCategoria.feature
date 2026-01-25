#language: pt-br

Funcionalidade: Edição Categoria

Cenário: Sucesso ao editar uma categoria
	Dado que existe uma categoria 'Laser'
	E que o usuário está editando a categoria 'Laser'
	Quando o usuário alterar o nome da categoria para 'Lazer'
	E o usuário salvar a categoria
	Então o sistema deverá editar a categoria como esperado