using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRPG
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
        static string barbaro = @"
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

        public static string Mago { get => mago; }
        public static string Barbaro { get => barbaro; }
    }
}
