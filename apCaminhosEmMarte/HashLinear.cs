using apCaminhosEmMarte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


  public class HashLinear<Tipo> : ITabelaDeHash<Tipo>
    where Tipo : IRegistro<Tipo>
  {
        private const int SIZE = 131; // para gerar mais colisões; o ideal é primo > 100

        private int tamanhoEmUso;

        Tipo[] dados;

        public HashLinear()
        {
            dados = new Tipo[SIZE];
            tamanhoEmUso = 0;
        }


        public int Hash(string chave)
        {
            // PARA ELEMENTOS JÁ ARMAZENADOS
            for(int i = 0; i < dados.Length; i++)
            {
                if (dados[i] != null && dados[i].Chave.Equals(chave)) { return i; }
            }


            long tot = 0;
            for (int i = 0; i < chave.Length; i++)
                tot += 37 * tot + (char)chave[i];

            tot = tot % dados.Length;
            if (tot < 0)
                tot += dados.Length;

            int posicaoInicio = (int)tot;
            int posicaoAtual = posicaoInicio;
            int qttdColisoes = 0;



            while (dados[posicaoAtual] != null && (posicaoAtual != posicaoInicio || qttdColisoes == 0))
            {
                qttdColisoes++;
                posicaoAtual++;
            }

            return posicaoAtual;

        }

        public void Inserir(Tipo item)
        {
            if (!EstaCheio())
            {
                int valorDeHash = Hash(item.Chave);
                dados[valorDeHash] = item;
                tamanhoEmUso++;
            }
        }

        public bool EstaCheio()
        {
            if (dados.Length == tamanhoEmUso) { return true; }

            return false;
        }

        public bool Remover(Tipo item)
        {
            int onde = 0;
            if (!Existe(item, out onde))
                return false;

            dados[onde] = default(Tipo);
            return true;
        }

        public bool Existe(Tipo item, out int posicao)
        {
            posicao = Hash(item.Chave);
            return dados[posicao].Equals(item);
        }

        public List<Tipo> Conteudo()
        {
            List<Tipo> saida = new List<Tipo>();
            for (int i = 0; i < dados.Length; i++)
                if (dados[i] !=null )
                {
                    saida.Add(dados[i]);
                }
            return saida;
        }
    }

