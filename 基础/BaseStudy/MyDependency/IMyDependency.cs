namespace BaseStudy 
{
    public interface IMyDependency
    {
        void WriteMessage();
    }
    public class MyDependency : IMyDependency
    {
        public void WriteMessage()
        {
            throw new NotImplementedException();
        }
    }
    public class DifferentDependency : IMyDependency
    {
        public void WriteMessage()
        {
            throw new NotImplementedException();
        }
    }
    
}
