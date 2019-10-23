# sudoku
Sudoku Game

The game is done using following technology stack:
C#, ASP.Net, WebAPI MVC, HTML Javascript Jquery bootstrap.


This is a web project can be build and debug from visual studio / visual studio community

Please run a clean build before building the project.


------------------------------------------------------------------------------------------------

Initial board is in solution>Controllers>ValuesController
readonly int[,] board = new int[,]
            {
            {0, 0, 6, 5, 0, 8, 4, 0, 0},
            {5, 2, 0, 0, 0, 0, 0, 0, 0},
            {0, 8, 7, 0, 0, 0, 0, 3, 1},
            {0, 0, 3, 0, 1, 0, 0, 8, 0},
            {9, 0, 0, 0, 6, 3, 0, 0, 5},
            {0, 5, 0, 0, 9, 0, 6, 0, 0},
            {1, 3, 0, 0, 0, 0, 2, 5, 0},
            {0, 0, 0, 0, 0, 0, 0, 7, 4},
            {0, 0, 5, 2, 0, 6, 3, 0, 0}
            };
            
Which can be used to have a new board published.

---------------------------------------------------------------------------------------------------
clicking the solve button will call the values web api solve the board and return values of the solved board.

There are two apis which are written 
1. to initialize the board
2. to solve the initialized board.

have used backtracking algorithm to solve the soduku missing values. 

APIs
GET/api/values/
return a preconfigured board.

GET/api/values/?recieveString=customBoard&id=9
accepts a board in string format and return the result. 

