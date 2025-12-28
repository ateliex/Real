#language: pt-br

Funcionalidade: Previsão Inteligente

Cenário de Fundo:
	Dado que existe as seguintes categorias:
		| Nome     |
		| Roupa    |
		| Mercado  |
		| Refeição |
	E que existe as seguintes contas:
		| Nome      |
		| Carteira  |
		| Cartão C6 |

Cenário: Sucesso ao prever uma finança
	Dado que existe uma previsão indeterminada de R$ 1000,00 da categoria 'Mercado'
	Quando eu lançar uma finança de R$ 200,00 na 'Carteira' da categoria 'Mercado'
	Então o valor da previsão inteligente deverá ser de R$ 800,00
