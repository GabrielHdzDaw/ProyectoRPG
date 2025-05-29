using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG.Recursos
{
    abstract class Sprites
    {
        static string mago = @"
                    ____ 
                  .'* *.'
               __/_*_*(_
              / _______ \
             _\_)/___\(_/_ 
            / _((\- -/))_ \
            \ \())(-)(()/ /
             ' \(((()))/ '
            / ' \)).))/ ' \
           / _ \ - | - /_  \
          (   ( .;''';. .'  )
          _\""__ /    )\ __""/_
            \/  \   ' /  \/
             .'  '...' ' )
              / /  |  \ \
             / .   .   . \
            /   .     .   \
           /   /   |   \   \
         .'   /    b    '.  '.
     _.-'    /     Bb     '-. '-._ 
 _.-'       |      BBb       '-.  '-. 
(________mrf\____.dBBBb.________)____)
";
        static string caballero = @"
  ,   A           {}
 / \, | ,        .--.
|    =|= >      /.--.\
 \ /` | `       |====|
  `   |         |`::`|
      |     .-;`\..../`;_.-^-._
     /\\/  /  |...::..|`   :   `|
     |:'\ |   /'''::''|   .:.   |
      \ /\;-,/\   ::  |..:::::..|
      |\ <` >  >._::_.| ':::::' |
      |  `""`  /   ^^  |   ':'   |
      |       |       \    :    /
      |       |        \   :   /
      |       |___/\___|`-.:.-`
      |        \_ || _/    `
      |        <_ >< _>
      |        |  ||  |
      |        |  ||  |
      |       _\.:||:./_
      |      /____/\____\
";
        static string elfo = @"
     .      .
     |\____/|
     | _  _ |
     \ 0  0 /
      | ¯¯ |
   ___/\__/\____   \
  /             \   \
 /  \         /  \   \
|    \___/___/(   |   |
\    |  }{  |   \  )  |-->
 \   |  }{  |    \  \ |
  \  |;;;;;;;\    \  /
   \ /;;;;;;;;|  [,,,]
     |;;;;;;/ |    /
     ||;;|\   |   /
     ||;;/|   /
     \_|:||__|
      \ ;||  /
      /= || =\
     | = /\ = |
    /___/  \___\
";

        static string picaro = @"
        __    ___
       |__|  /   \
        \ \ | 0  -|
        _\ \)  |  (
       |  --'\ ~ /`--.
       .-\- / --- \  /`.
      _/_|_.-' : `-._|__\_
     <__>'\    :   / `<___>
     / /   >=======<  /  /
    /.'   /  ,-:-.  \/=,'
   |_|   |__/v^v^v\__)_\
   \ /    |V^V^V^V^V|\_/
   (-)    \`---|---'/\ \
  /   \    \-._|_,-/  \ \
 (     )   |__ | __|   \/
(  $$$  ) <___ X ___>
 (     )   \.. | ../
  -----     \  |  /
            /V | V\
           /  /|\  \
          '--'   `--`
";
        static string fantasma = @"
     .-.
   .'   `.
   :0 0   :
   : o    `.
  :         ``.
 :             `.
:  :         .   `.
:   :          ` . `.
 `.. :            `. ``;
    `:;             `:'
       :              `.
        `.              `.     .
          `'`'`'`---..,___`;.-'";

        static string fantasmicos = @"
                      .-.
         heehee      /aa \_
                   __\-  / )                 .-.
         .-.      (__/    /        haha    _/oo \
       _/ ..\       /     \               ( \v  /__
      ( \  u/__    /       \__             \/   ___)
       \    \__)   \_.-._._   )  .-.       /     \
       /     \             `-`  / ee\_    /       \_
    __/       \               __\  o/ )   \_.-.__   )
   (   _._.-._/     hoho     (___   \/           '-'
    '-'                        /     \
                             _/       \    teehee
                            (   __.-._/";

        static string esqueleto = @"
      .-.
     (o.o)
      |=|
     __|__
   //.=|=.\\
  // .=|=. \\
  \\ .=|=. //
   \\(_=_)//
    (:| |:)
     || ||
     () ()
     || ||
     || ||
l42 ==' '=='";
        static string centauro = @"
         =*===
       $$- - $$$
       $ <    D$$
       $ -   $$$
 ,     $$$$  |
///; ,---' _ |----.
 \ )(           /  )
 | \/ \.   '  _.|  \              $
 |  \ /(   /    /\_ \          $$$$$
  \ /  (       / /  )         $$$ $$$
       (  ,   /_/ ,`_,-----.,$$  $$$
       |   <----|  \---##     \   $$
       /         \\\           |    $
      '   '                    |
      |                 \      /
      /  \_|    /______,/     /
     /   / |   /    |   |    /
    (   /--|  /.     \  (\  (_
     `----,( ( _\     \ / / ,/
           | /        /,_/,/
          _|/        / / (
         / (        ^-/, |
        /, |          ^-  
        ^-";

        static string espadaCorda = @"
      /| ________________
O|===|* >________________>
      \|
";

        static string espadaLarga = @"
         />_________________________________
[########[]_________________________________>
         \>
";

        static string katana = @"
            /\
/vvvvvvvvvvvv \--------------------------------------,
`^^^^^^^^^^^^ /=====================================""
            \/
";
        static string punalBarato = @"
   '
=={==========-
   `
";
        static string punalEngarzado = @"
     #
O%%%%#============--
     #
";
        static string punalElectrico = @"
          ./~
(=@@@@@@@=[}=================--
          `\_
";
        static string arcoCorto = @"
   (
    \
     )
##-------->
     )
    /
   (
";

        public static string Mago { get => mago; }
        public static string Caballero { get => caballero; }
        public static string Elfo { get => elfo; }
        public static string Picaro { get => picaro; }
        public static string Fantasma { get => fantasma; }
        public static string Fantasmicos { get => fantasmicos; }
        public static string Esqueleto { get => esqueleto; }
        public static string Centauro { get => centauro; }
        public static string EspadaCorda { get => espadaCorda; }
        public static string EspadaLarga { get => espadaLarga; }
        public static string Katana { get => katana; }
        public static string PunalBarato { get => punalBarato; }
        public static string PunalEngarzado { get => punalEngarzado; }

    }
}
