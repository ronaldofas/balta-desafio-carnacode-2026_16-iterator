namespace DesignPatternChallenge.IteratorDemo
{
    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }
}
