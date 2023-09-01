using System;

namespace Game_step {

class Game {
    public int[,] plateau;
    public int nb_game;

    public Game(int nb_game)	
    {
        this.plateau = new int[3,3] ;
        this.nb_game = nb_game;
		run();
    }

    public void run()
    {
        for (int i = 0; i < this.nb_game; i++)
        {
			init_plateau();
            while (continue_game())
            {
                display_plateau();
                player_action();
                bot_action();
            }
        }
    }

    public void init_plateau()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                this.plateau[i,j] = 0;
            }
        }
    }

    public void player_action()
    {
		int action = 0;
        Console.WriteLine("Enter positions (ex : 12):");
        action = int.Parse(Console.ReadLine());
        this.plateau[(action / 10) -1 ,(action % 10 -1)] = 1;
    }

    public void bot_action()
    {
        bool action = false;
        int test_possibility;
		int test_possibility_2;
        while (!action)
        {
            Random rnd = new Random();
            test_possibility = rnd.Next() % 3;
            test_possibility_2 = rnd.Next() % 3;
            if (this.plateau[test_possibility,test_possibility_2] == 0)
            {
                this.plateau[test_possibility,test_possibility_2] = 2;
                action = true;
            }
        }
    }

    public void display_plateau()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3 ; j++)
            {
                switch (this.plateau[i,j])
                {
                    case 0:
                        Console.Write("_");
                        break;
                    case 1:
                        Console.Write("O");
                        break;
                    case 2:
                        Console.Write("X");
                        break;
                }
            }
            Console.Write("\n");
        }
    }

    public bool is_complete_game()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (this.plateau[i,j] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool is_win_game(int player)
    {
        for (int i = 0 ; i < 3 ; i++)
        {
            // Lign test
            if (this.plateau[i,0] == player && this.plateau[i,1] == player && this.plateau[i,2] == player)
            {
                return true;
            }
            // Column test
            if (this.plateau[0,i] == player && this.plateau[1,i] == player && this.plateau[2,i] == player)
            {
                return true;
            }
        }
        // Diagolanes test
        if (this.plateau[0,0] == player && this.plateau[1,1] == player && this.plateau[2,2] == player)
        {
            return true;
        }

        if (this.plateau[2,2] == player && this.plateau[1,1] == player && this.plateau[0,0] == player)
        {
            return true;
        }

        return false;
    }

    public bool continue_game()
    {
        if (is_win_game(1))
        {
            Console.WriteLine("Player win");
            return false;
        }

        if (is_win_game(2))
        {
            Console.WriteLine("Bot win");
            return false;
        }

        if (is_complete_game())
        {
            Console.WriteLine("No winner");
            return false;
        }
        return true;
    }
}
}