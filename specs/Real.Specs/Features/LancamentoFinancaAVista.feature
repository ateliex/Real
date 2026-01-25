#language: pt-br

Funcionalidade: Lançamento Finança à Vista

Cenário: Sucesso ao lançar uma finança à vista
	Dado que existe a seguinte conta:
		| Nome     |
		| Carteira |
	E que existe a seguinte categoria:
		| Nome    |
		| Salário |
	E que o usuário está fazendo um novo lançamento de finança à vista
	Quando o usuário salvar o lançamento com os seguintes dados:
		| Conta    | Data       | Categoria | Descrição  | Valor   | Tipo    |
		| Carteira | 15/03/2025 | Salário   | Depósito _ | 1500,00 | Receita |
	Então a finança à vista deverá ser lançada como:
		| Conta    | Data       | Categoria | Descrição  | Valor   | Tipo    |
		| Carteira | 15/03/2025 | Salário   | Depósito _ | 1500,00 | Receita |
