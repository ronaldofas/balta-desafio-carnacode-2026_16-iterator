using System.Collections.Generic;

namespace DesignPatternChallenge.IteratorDemo
{
    public class RefactoredMusicLibrary : IAggregate<Song>
    {
        // Estrutura interna complexa Ocultada!
        private readonly Dictionary<string, List<Song>> _songsByGenre;
        private readonly Dictionary<string, List<Song>> _songsByArtist;

        public RefactoredMusicLibrary()
        {
            _songsByGenre = new Dictionary<string, List<Song>>();
            _songsByArtist = new Dictionary<string, List<Song>>();
        }

        public void AddSong(Song song)
        {
            if (!_songsByGenre.ContainsKey(song.Genre))
                _songsByGenre[song.Genre] = new List<Song>();
            _songsByGenre[song.Genre].Add(song);

            if (!_songsByArtist.ContainsKey(song.Artist))
                _songsByArtist[song.Artist] = new List<Song>();
            _songsByArtist[song.Artist].Add(song);
        }

        // --- FÁBRICA DE ITERADOR ---
        // Agora, iterar sobre a biblioteca devolve o iterador achatado,
        // não vazando os Dicionários complexos para o cliente.
        public IIterator<Song> CreateIterator()
        {
            return new MusicLibraryIterator(_songsByGenre);
        }
    }
}
