using System;

namespace CavaloNoTabuleiro
{
    class Program
    {
        static int Size; // tamanho do tabuleiro
        static int[,] Board; // tabuleiro
        static readonly int[] MoveX = { 2, 1, -1, -2, -2, -1, 1, 2 }; // movimento do cavalo na direção X
        static readonly int[] MoveY = { 1, 2, 2, 1, -1, -2, -2, -1 }; // movimento do cavalo na direção Y

        static void Main(string[] args)
        {
            Size = 8; // tamanho do tabuleiro
            Board = new int[Size, Size];

            // Inicializa o tabuleiro com valores -1 (indicando que nenhuma casa foi visitada)
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Board[i, j] = -1;
                }
            }

            // Define a posição inicial do cavalo no tabuleiro
            int startX = 3;
            int startY = 5;
            Board[startX, startY] = 0; // marca a posição inicial como visitada

            if (SolveKnightTour(startX, startY, 1)) // chama a função para resolver o problema
            {
                PrintBoard(); // imprime a solução
            }
            else
            {
                Console.WriteLine("Não há solução para o problema.");
            }
        }

        // Busca em profundidade para encontrar um caminho válido para o cavalo percorrer todas as casas do tabuleiro exatamente uma vez
        static bool SolveKnightTour(int x, int y, int moveCount)
        {
            if (moveCount == Size * Size) // todas as casas foram visitadas
            {
                return true;
            }

            for (int i = 0; i < MoveX.Length; i++) // tenta todos os movimentos possíveis do cavalo
            {
                int nextX = x + MoveX[i];
                int nextY = y + MoveY[i];

                if (IsSafe(nextX, nextY)) // verifica se o próximo movimento é válido
                {
                    Board[nextX, nextY] = moveCount; // marca o próximo movimento como visitado

                    if (SolveKnightTour(nextX, nextY, moveCount + 1)) // recursivamente faz o próximo movimento
                    {
                        return true;
                    }

                    Board[nextX, nextY] = -1; // desmarca o próximo movimento se a recursão falhar
                }
            }

            return false; // retorna falso se não há solução
        }

        static bool IsSafe(int x, int y)
        {
            // verifica se a posição está dentro dos limites do tabuleiro e se a casa não foi visitada
            return (x >= 0 && x < Size && y >= 0 && y < Size && Board[x, y] == -1);
        }

        static void PrintBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(Board[i, j].ToString().PadLeft(2) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
