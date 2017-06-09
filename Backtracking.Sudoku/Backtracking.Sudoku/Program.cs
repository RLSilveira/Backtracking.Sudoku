using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backtracking.Sudoku
{
    class Program
    {

        class Tabuleiro
        {
            int[][] tab;

            public Tabuleiro()
            {
                tab = new int[9][];
                for (int i = 0; i < tab.Length; i++)
                {
                    tab[i] = new int[9];
                    for (int j = 0; j < tab[i].Length; j++)
                    {
                        tab[i][j] = 0;
                    }
                }
            }

            public bool Insere(int linha, int coluna, int valor)
            {
                if (tab[linha][coluna] != 0) return false;

                Console.WriteLine(" Insert: [{0}][{1}]: {2}", linha + 1, coluna + 1, valor);
                tab[linha][coluna] = valor;

                Console.Write(this);

                bool r = Valida(linha, coluna, valor);

                if (!r)
                {
                    Console.WriteLine("-Delete: [{0}][{1}]: {2}", linha + 1, coluna + 1, valor);
                    tab[linha][coluna] = 0;
                    return false;
                }

                return true;

            }

            public void Remove(int linha, int coluna)
            {
                if (!(linha >= 9 || coluna >= 9))
                {
                    tab[linha][coluna] = 0;
                }
            }

            private bool Valida(int linha, int coluna, int valor)
            {
                return
                    ValidaLinha(linha, valor)
                    &&
                    ValidaColuna(coluna, valor);
            }

            bool ValidaLinha(int linha, int valor)
            {
                int c = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (tab[linha][i] == valor) c++;
                }

                return c == 1;
            }

            bool ValidaColuna(int coluna, int valor)
            {
                int c = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (tab[i][coluna] == valor) c++;
                }

                return c == 1;
            }

            bool ValidaBloco()
            {
                return true;
            }

            public override string ToString()
            {
                string ret = "";
                int i, j;
                for (i = 0; i < 9; i++)
                {
                    for (j = 0; j < 9; j++)
                    {
                        ret += string.Format("{0} ", tab[i][j]);
                    }
                    ret += string.Format("\n");
                }
                ret += string.Format("\n\n");

                return ret;
            }
            
        }


        static void Main(string[] args)
        {
            Tabuleiro t = new Tabuleiro();

            for (int i = 1; i < 10; i++)
            {
                var b = BacktrackingSudoku(t, 0, i);
                if (b) break;
            }

            Console.WriteLine("Acabou");
            Console.ReadKey();
        }

        static bool BacktrackingSudoku(Tabuleiro tab, int linha, int coluna, int valor = 999)
        {
            if (linha >= 9)
            {
                Console.WriteLine("Terminou");
                Console.Write(tab);
                Console.ReadKey();
                return true;
            }

            if (coluna >= 9) return BacktrackingSudoku(tab, linha + 1, 0);


            for (int i = 1; i < 10; i++)
            {
                if (tab.Insere(linha, coluna, i))
                {
                    var b2 = BacktrackingSudoku(tab, linha, coluna + 1);
                    if (b2) return true;
                    else tab.Remove(linha, coluna + 1);

                }
            }

            return false;

        }

    }
}
