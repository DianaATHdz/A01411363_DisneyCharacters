using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THICC : MonoBehaviour {
    AudioSource audioSource;
    #region ClassAttributes
    //class attributes
    //menuHint will tell the user that they can type menu whenever they want
    //to return to the main menu.
    const string menuHint = "Para ir al menu inicial escribe 'menu'";
    const string retryHint = "Para volver a intentar escribe 'retry'";
    //These arrays will hold the characters and the questions
    string[] character = { "Woody", "Blanca Nieves", "Ariel", "Baymax", "Simba", "Nemo"};
    string[] questions = { "Vaquero que emprende grandes viajes junto con su mejor amigo espacial. ",
    "Blanca como la nieve, sonrosada como la sangre y de cabellos negros como el ébano.",
    "Si tan sólo pudiera hacerle ver, que no veo las cosas como él lo hace. No es posible que un mundo que hace tantas maravillas sea tan malo",
    "En una escala del uno al diez, ¿Cómo calificarías tu dolor?",
    "Yo quisiera ya... Ser el Rey",
    "Tiburoncin ujaja"};
    //These are the quotes that will display if the user writes the easter egg
    string[] Easteregg = { "El pasado puede doler pero, tal y como yo lo veo, puedes: o huir de él o aprender.  -El rey León",
    "La flor que florece en la adversidad es la más rara y hermosa de todas.  -Mulan",
    "Algunas veces el camino correcto no es el más fácil.  -Pocahontas",
    "Eres más valiente de lo que crees, más fuerte de lo que pareces y más inteligente de lo que piensas.  -Winnie the Pooh",
    "Cuando amas a alguien, permanece dentro de tu corazón para siempre.  -Tierra de Osos",
    "Si hay una cosa que nadie ha podido comprar con dinero, ésa es el movimiento de la cola de un perro.  -La dama y el vagabundo"};

    //Here I declare an enumerated type to represent the different game
    //states, and I declare a variable to hold the current game state
    int tries_limit = 3;
    int tries;
    int CharID;
    enum GameState { MainMenu, Character, Win, Lose };
    GameState currentState;
    string personaje;

    #endregion
    // Use this for initialization
    void Start () {
        //Audios
        audioSource = GetComponent<AudioSource>();
        ShowMainMenu();		
	}

    private void ShowMainMenu()
    {
        //These are the messages that will be print in the menu screen
        currentState = GameState.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("             ¡BIENVENIDOS!        ");
        Terminal.WriteLine("Qué tan fan eres de Disney? Adivina el personaje!");
        Terminal.WriteLine("Para terminar el juego escribe 'quit'");
        Terminal.WriteLine(menuHint);
        //In the time you write start, the game's screen will show up
        Terminal.WriteLine("Escribe 'start' para empezar");
    }

    // Update is called once per frame
    void Update () {
		
	}
    // The OnUserInput mehtod is special. It is called everytime the user
    // hits enter on their keyboard. This method will let us evaluate the
    // input data and act accordingly.
    void OnUserInput(string input)
    { 
     // If user inputs the "menu" keyword, then we call the
        // ShowMainMenu() method once more
        if (currentState == GameState.Win || currentState == GameState.Lose)
        {   
            //if the user writes menu
            if (input == "menu")
            {
                ShowMainMenu();
            }
            //if the user wants to retry after they fail to guess
            else if (input == "retry")
            {
                Debug.Log("retry?");
                RunMainMenu("start");
            }
        }
        else if (input== "menu")
        {
            ShowMainMenu();
        }
        //This is the easter egg :D
        else if (input== "disney" || input == "Disney" || input== "DISNEY")
        {
            Terminal.WriteLine(Easteregg[UnityEngine.Random.Range(0, Easteregg.Length)]);
        }

        // However, if the user types quit, close, exit, then we try to close
        // our game. If the game is played on a Web browser, then we ask the
        // user to close the browser's tab
        else if (input == "quit" || input == "close" || input == "exit")
        {
            Terminal.WriteLine("Please, close the browser's tab");
            Application.Quit();
        }

        // If the user inputs anything that is not menu, quit, close or exit,
        // then we are going to handle that input depending on the game state.
        // If the game state is still MainMenu, then we call the RunMainMenu()
        // method.
        else if (currentState == GameState.MainMenu)
        {
            RunMainMenu(input);
        }
        // But if the current game state is Password, then we call the
        // CheckPassword() Method
        else if (currentState == GameState.Character)
        {
            CheckCharacter(input);
        }
    }

    void CheckCharacter(string input)
    {
    
    tries++;
    
    //If the user writes the right character
    if (input == personaje)
    {   
        //The win screen shows, hurray!!
        DisplayWinScreen();
    }
    //If you don't get the righ character in the first time
    else
    {   //The user still has 3 more chances
        AskForPassword();
    }
}

    //This is the Win Screen and the things that will display 
    private void DisplayWinScreen()
    {
        currentState = GameState.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }
    //This is the Lose Screen and the things that will display
    private void DisplayLoseScreen()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Oh no :( ");
        Terminal.WriteLine("No te preocupes, ¡Sigue intentando!");
        currentState = GameState.Lose;
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine(retryHint);
    }

    //This is the Reward and the things that will display
    //In this screen you can have a mercy
    private void ShowLevelReward()
    {
        Terminal.WriteLine("Yey, ¡Felicidades por ganar!");
        //Terminal.WriteLine("Que la magia siempre este en ti");
        Terminal.WriteLine(@"     _ _                      
    | (_)                     
  __| |_ ___ _ __   ___ _   _ 
 / _` | / __| '_ \ / _ \ | | |
| (_| | \__ \ | | |  __/ |_| |
 \__,_|_|___/_| |_|\___|\__, |
                         __/ |
                        |___/ ");
        Terminal.WriteLine("");
}

    void RunMainMenu(string input)
    {
    // We fisrt check that the input is a valid input
    bool isValidInput = (input == "start");

    // If the user inputs a valid level, we convert that input to an int
    // value and then we call the AskForPassword() method.
    if (isValidInput)
    {
        tries = 0;
        AskForPassword();
    }
    // If not, then we just ask them to enter a valid level.
    else
    {
        Terminal.WriteLine("Para empezar escribe 'start'");
    }
}

private void AskForPassword()
{
    if (currentState != GameState.Character)
    {
        Terminal.ClearScreen();
        SetRandomPassword();
    }
    // We set our current game state as Password
    currentState = GameState.Character;

    //We clear the terminal screen
    Terminal.ClearScreen();

    //We call the SetRandomPassword() method
    if (tries > tries_limit-1)
    {
        DisplayLoseScreen();
    }
    else
    {
        Terminal.WriteLine(questions[CharID]);
        Terminal.WriteLine("Aun tienes " + (tries_limit - tries) + " intentos.");
        Terminal.WriteLine(menuHint);
    }
}

    private void SetRandomPassword()
    {
        //Dependin on the selected  level, we choose a random password to crack
        CharID = UnityEngine.Random.Range(0, character.Length);
        personaje = character[CharID];
    }
}
