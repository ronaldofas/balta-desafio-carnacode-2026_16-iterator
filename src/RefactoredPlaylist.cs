using System.Collections.Generic;

namespace DesignPatternChallenge.IteratorDemo
{
    public class RefactoredPlaylist : IAggregate<Song>
    {
        public string Name { get; private set; }
        
        // A coleção agora é privada, não exposta ao mundo externo!
        private readonly List<Song> _songs;

        public RefactoredPlaylist(string name)
        {
            Name = name;
            _songs = new List<Song>();
        }

        public void AddSong(Song song)
        {
            _songs.Add(song);
        }

        // --- FÁBRICAS DE ITERADORES ---

        // O Iterador padrão, exigido pela interface IAggregate
        public IIterator<Song> CreateIterator()
        {
            return new SequentialPlaylistIterator(_songs);
        }

        public IIterator<Song> CreateShuffleIterator()
        {
            return new ShufflePlaylistIterator(_songs);
        }

        public IIterator<Song> CreateGenreIterator(string genre)
        {
            return new GenrePlaylistIterator(_songs, genre);
        }

        public IIterator<Song> CreateYearIterator(int yearThreshold, bool isOlderThan)
        {
            return new YearPlaylistIterator(_songs, yearThreshold, isOlderThan);
        }
    }
}
