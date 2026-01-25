#language: pt-br

Funcionalidade: Criação Conta

Esquema do Cenário: Sucesso ao criar uma conta
	Dado que o usuário está criando uma nova conta
	E que o usuário preencheu o tipo da conta como '<Tipo>'
	E que o usuário preencheu o nome da conta como '<Nome>'
	Quando o usuário salvar a conta
	Então a conta deverá ser criada com sucesso

Exemplos:
	| Tipo            | Nome          |
	| CreditoAReceber | Contra-cheque |
	| CreditoAPagar   | Cartão Nubank |
	| Debito          | Carteira      |