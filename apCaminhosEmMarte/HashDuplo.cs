using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhosEmMarte
{
  public class HashDuplo<Tipo> : ITabelaDeHash<Tipo>
    where Tipo : IRegistro<Tipo>
  {
        private Tipo[] tabela; // Array para armazenar os elementos
        private int capacidade; // Capacidade da tabela
        private int tamanho;    // Número de elementos na tabela

        public HashDuplo()
        {
        }

        public HashDuplo(int capacidade)
        {
            this.capacidade = capacidade;
            this.tabela = new Tipo[capacidade];
            this.tamanho = 0;
        }

        public List<Tipo> Conteudo()
        {
            // Cria uma lista para armazenar os elementos da tabela
            List<Tipo> elementos = new List<Tipo>();

            // Percorre a tabela e adiciona os elementos não nulos à lista
            for (int i = 0; i < capacidade; i++)
            {
                if (tabela[i] != null)
                {
                    elementos.Add(tabela[i]);
                }
            }

            return elementos;
        }
        public bool Existe(Tipo item, out int onde)
        {
            int posicao = Hash(item.Chave); // Calcula a posição na tabela

            int h2 = Hash2(item.Chave); // Segundo hash para sondagem dupla

            // Percorre a tabela usando sondagem dupla
            int tentativas = 0; // Contador de tentativas para evitar loop infinito
            while (tabela[posicao] != null && !tabela[posicao].Equals(item) && tentativas < capacidade)
            {
                posicao = (posicao + h2) % capacidade; // Próxima posição na tabela usando segundo hash
                tentativas++;
            }

            // Verifica se o item foi encontrado
            if (tabela[posicao] != null && tabela[posicao].Equals(item))
            {
                onde = posicao; // Define a posição onde o item foi encontrado
                return true;
            }

            onde = -1; // Item não encontrado
            return false;
        }

        private int Hash(string chave)
        {
            throw new NotImplementedException();
        }

        private int Hash2(string chave)
        {
            throw new NotImplementedException();
        }

        public void Inserir(Tipo item)
        {
            // Verifica se o item já existe na tabela
            int posicao;
            if (Existe(item, out posicao))
            {
                // Se o item já existe, não faz nada
                return;
            }

            // Insere o item na primeira posição disponível após o cálculo do hash
            int posicaoInsercao = Hash(item.Chave);
            int h2 = Hash2(item.Chave); // Segundo hash para sondagem dupla
            int tentativas = 0; // Contador de tentativas para evitar loop infinito
            while (tabela[posicaoInsercao] != null && tentativas < capacidade)
            {
                posicaoInsercao = (posicaoInsercao + h2) % capacidade; // Próxima posição na tabela usando segundo hash
                tentativas++;
            }

            // Verifica se encontrou uma posição vazia para inserção
            if (tabela[posicaoInsercao] == null)
            {
                tabela[posicaoInsercao] = item;
                tamanho++;
            }
            else
            {
                // Caso a tabela esteja cheia, podemos lançar uma exceção ou redimensionar a tabela
                throw new InvalidOperationException("A tabela está cheia. Não é possível inserir o item.");
            }
        }
        public bool Remover(Tipo item)
        {
            // Verifica se o item existe na tabela
            int posicao;
            if (!Existe(item, out posicao))
            {
                // Se o item não existe, não faz nada
                return false;
            }

            // Remove o item da posição encontrada
            tabela[posicao] = default(Tipo);
            tamanho--;
            return true;
        }

        // Função de hash principal
        private int Hash(int chave)
        {
            return chave % capacidade;
        }

        // Segunda função de hash para sondagem dupla
        private int Hash2(int chave)
        {
            // Escolhe um valor para ser usado como incremento para a sondagem dupla
            // Deve ser um número primo menor que a capacidade da tabela
            return 7 - (chave % 7);
        }
    }
}