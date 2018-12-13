using System;
using Aiv.Draw;

namespace Memory1A
{
    class Program
    {
        static void PutPixel(Window window, int x, int y, byte r, byte g, byte b)
        {
            if (x < 0 || x > window.width - 1)
            {
                return;
            }

            if (y < 0 || y > window.height - 1)
            {
                return;
            }

            int position = (window.width * y * 3) + (x * 3);
            window.bitmap[position] = r;
            window.bitmap[position + 1] = g;
            window.bitmap[position + 2] = b;
        }
        static void DrawHorizontalLine(Window window, int x, int y, int width, byte r, byte g, byte b)
        {
            for (int i = x; i < x + width; i++)
            {
                PutPixel(window, i, y, r, g, b);
            }
        }
        static void DrawVerticalLine(Window window, int x, int y, int height, byte r, byte g, byte b)
        {
            for (int i = y; i < y + height; i++)
            {
                PutPixel(window, x, i, r, g, b);
            }
        }
        static void DrawRectangle(Window window, int x, int y, int width, int height, byte r, byte g, byte b)
        {
            DrawHorizontalLine(window, x, y, width, r, g, b);
            DrawHorizontalLine(window, x, y + height, width, r, g, b);
            DrawVerticalLine(window, x, y, height, r, g, b);
            DrawVerticalLine(window, x + width, y, height, r, g, b);
        }

        static void DrawSolidRectangle(Window window, int x, int y, int width, int height, byte r, byte g, byte b)
        {
            for (int i = x; i < width + x; i++)
            {
                DrawVerticalLine(window, i, y, height, r, g, b);
            }
        }

        static void ClearScreen(Window window)
        {
            for (int i = 0; i < window.bitmap.Length; i++)
            {
                window.bitmap[i] = 0;
            }
        }

        struct Vector2
        {
            public int X;
            public int Y;
        }

        struct Color
        {
            public byte R;
            public byte G;
            public byte B;
        }

        struct Card
        {
            public int Value;
            public bool Covered;
            public Vector2 Position;
            public Color Color;

        }

        struct Deck
        {
            public Card[] Cards;
            public int CardWidth;
            public int CardHeight;
            public int Rows;
            public int Columns;
        }

        static void InitCard(out Card c, int value, Color color, Vector2 pos)
        {
            c.Covered = true;
            c.Value = value;
            c.Color = color;
            c.Position = pos;
        }

        static void InitDeck(out Deck d, int rows, int columns, Random random, int cardWidth = 100, int cardHeight = 100, int padding = 10)
        {
            d.CardWidth = cardWidth;
            d.CardHeight = cardHeight;
            d.Columns = columns;
            d.Rows = rows;
            d.Cards = new Card[rows * columns];
            for (int i = 0; i < d.Cards.Length / 2; i++)
            {
                Color nextColor;
                nextColor.R = (byte)random.Next(50, 255);
                nextColor.G = (byte)random.Next(50, 255);
                nextColor.B = (byte)random.Next(50, 255);
                Vector2 nextPos;
                nextPos.X = padding + (i % columns) * (cardWidth + padding);
                nextPos.Y = padding + (i / columns) * (cardHeight + padding);
                InitCard(out d.Cards[i], i, nextColor, nextPos);

                nextPos.Y += rows / 2 * (cardHeight + padding);
                InitCard(out d.Cards[i + d.Cards.Length / 2], i, nextColor, nextPos);
            }

        }

        static void Shuffle(ref Deck d, Random random)
        {
            for (int i = 0; i < d.Cards.Length; i++)
            {
                int randomIndex = random.Next(i, d.Cards.Length);
                Swap(ref d.Cards[i], ref d.Cards[randomIndex]);
            }
        }

        static void Swap(ref Card a, ref Card b)
        {
            Color tempColor = a.Color;
            int tempValue = a.Value;

            a.Value = b.Value;
            a.Color = b.Color;

            b.Value = tempValue;
            b.Color = tempColor;
        }

        static void DrawCard(Window window, Card c, int width, int height)
        {
            if (!c.Covered)
            {
                DrawSolidRectangle(window, c.Position.X, c.Position.Y, width, height, c.Color.R, c.Color.G, c.Color.B);
            }
            else
            {
                DrawSolidRectangle(window, c.Position.X, c.Position.Y, width, height, 255, 255, 255);
            }
        }
        static void DrawDeck(Window window, Deck d)
        {
            for (int i = 0; i < d.Cards.Length; i++)
            {
                DrawCard(window, d.Cards[i], d.CardWidth, d.CardHeight);
            }
        }

        static bool Contains(Vector2 pos, int width, int height, Vector2 point)
        {
            return (point.X >= pos.X && point.X <= pos.X + width) && (point.Y >= pos.Y && point.Y <= pos.Y + height);
        }




        static int GetRandomCard(Window window, Deck d, Random random)
        {
            Vector2 randomPos;
            randomPos.X = random.Next(window.width);
            randomPos.Y = random.Next(window.height);

            for (int i = 0; i < d.Cards.Length; i++)
            {
                if (Contains(d.Cards[i].Position, d.CardWidth, d.CardHeight, randomPos))
                {

                    return i;
                }
            }
            return -1;
        }

        static int GetSelectedCard(Window window, Deck d)
        {
            if (window.mouseLeft)
            {
                Vector2 mousePos;
                mousePos.X = window.mouseX;
                mousePos.Y = window.mouseY;

                for (int i = 0; i < d.Cards.Length; i++)
                {
                    if (Contains(d.Cards[i].Position, d.CardWidth, d.CardHeight, mousePos))
                    {

                        return i;
                    }
                }
            }
            return -1;
        }

        static void Main(string[] args)
        {
            int rows = 4;
            int columns = 4;
            const float DEFAULT_CLICK_COUNTDOWN = 0.2f;
            float clickCountDown = 0;
            bool isMyTurn = true;
            bool isAiTurn = false;
            int myPoints = 0;
            int AiPointS = 0;
            Random random = new Random();
            Deck d;
            InitDeck(out d, rows, columns, random);
            Shuffle(ref d, random);

            int[] selectedCards = new int[2];
            int nrSelectedCards = 0;

            Window window = new Window(450, 450, "DrawCane", PixelFormat.RGB);
            // Window window = new Window(1280, 720, "DrawCane", PixelFormat.RGB);
            int pairsToFind = d.Cards.Length / 2;

            while (pairsToFind > 0)
            {
                while (isMyTurn)
                {

                    if (window.GetKey(KeyCode.Esc))
                    {
                        break;
                    }

                    clickCountDown -= window.deltaTime;
                    if (nrSelectedCards < 2)
                    {
                        if (clickCountDown <= 0)
                        {
                            int currentCard = GetSelectedCard(window, d);
                            if (currentCard != -1)
                            {
                                clickCountDown = DEFAULT_CLICK_COUNTDOWN;
                                if (d.Cards[currentCard].Covered)
                                {
                                    d.Cards[currentCard].Covered = false;
                                    selectedCards[nrSelectedCards++] = currentCard;
                                }
                            }
                        }
                    }


                    // DrawSolidRectangle(window, 0, 0, window.width, window.height, (byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255));

                    ClearScreen(window);
                    DrawDeck(window, d);
                    window.Blit();

                    if (nrSelectedCards == 2)
                    {
                        if (d.Cards[selectedCards[0]].Value == d.Cards[selectedCards[1]].Value)
                        {
                            pairsToFind--;
                            myPoints++;
                            Console.WriteLine("USER " + myPoints + "  --------------  " + AiPointS + " BOT");
                            if (pairsToFind == 0 && myPoints > AiPointS)
                            {
                                Console.WriteLine("       Hai vinto secco! ");
                            }
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(1000);
                            d.Cards[selectedCards[0]].Covered = true;
                            d.Cards[selectedCards[1]].Covered = true;
                            isMyTurn = false;
                            isAiTurn = true;

                        }
                        nrSelectedCards = 0;
                    }

                }

                

                while (isAiTurn)
                {
                    if (nrSelectedCards < 2)
                    {

                        int currentCard = GetRandomCard(window, d, random);
                        if (currentCard != -1)
                        {

                            if (d.Cards[currentCard].Covered)
                            {
                                d.Cards[currentCard].Covered = false;
                                selectedCards[nrSelectedCards++] = currentCard;
                                System.Threading.Thread.Sleep(1000);
                            }
                        }

                    }


                    // DrawSolidRectangle(window, 0, 0, window.width, window.height, (byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255));

                    ClearScreen(window);
                    DrawDeck(window, d);
                    window.Blit();

                    if (nrSelectedCards == 2)
                    {
                        if (d.Cards[selectedCards[0]].Value == d.Cards[selectedCards[1]].Value)
                        {
                            pairsToFind--;
                            System.Threading.Thread.Sleep(1000);
                            AiPointS++;
                            Console.WriteLine("USER " + myPoints + "  --------------  " + AiPointS + " BOT");
                            if (pairsToFind == 0 && myPoints < AiPointS)
                            {
                                Console.WriteLine("      Sei na chiavica zì ");
                            }

                        }
                        else
                        {
                            System.Threading.Thread.Sleep(1000);
                            d.Cards[selectedCards[0]].Covered = true;
                            d.Cards[selectedCards[1]].Covered = true;
                            isMyTurn = true;
                            isAiTurn = false;

                        }
                        nrSelectedCards = 0;
                    }



                }
                

            }


        }

        
    }
}
