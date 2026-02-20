using System.Collections.Generic;
using System.Linq;

namespace DesignPatternChallenge.IteratorDemo
{
    public class MusicLibraryIterator : IIterator<Song>
    {
        // Ao invés de lidar com as chaves separadamente o tempo todo,
        // achatamos temporariamente a estrutura para a iteração.
        // Ou poderíamos manter o controle das chaves, mas para simplificar
        // iteraremos sobre todas as músicas de todos os gêneros (sem repetição).
        private readonly List<Song> _allSongs;
        private int _position = 0;

        public MusicLibraryIterator(Dictionary<string, List<Song>> songsByGenre)
        {
            // Achata o dicionário em uma única lista distinta para iteração
            _allSongs = songsByGenre.Values
                .SelectMany(list => list)
                .Distinct()
                .ToList();
        }

        public bool HasNext()
        {
            return _position < _allSongs.Count;
        }

        public Song Next()
        {
            var song = _allSongs[_position];
            _position++;
            return song;
        }

        public void Reset()
        {
            _position = 0;
        }
    }
}
