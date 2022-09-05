// See https://aka.ms/new-console-template for more information
using System.Collections;

//Console.WriteLine("Hello, World!");
 
public interface IObserver
{
    void Notify(object anObject);
}

//“被观察对象”接口
public interface IObservable
{
    void Register(IObserver anObserver);
    void UnRegister(IObserver anObserver);
}
//所有被观察对象的基类
public class ObservableImpl : IObservable
{

    //保存观察对象的容器
    protected Hashtable _observerContainer = new Hashtable();

    //注册观察者
    public void Register(IObserver anObserver)
    {
        _observerContainer.Add(anObserver, anObserver);
    }

    //撤销注册
    public void UnRegister(IObserver anObserver)
    {
         
        _observerContainer.Remove(anObserver);
    }

    //将事件通知观察者
    public void NotifyObservers(object anObject)
    {
        //枚举容器中的观察者，将事件一一通知给他们
        foreach (IObserver anObserver in _observerContainer.Keys)
        {
            anObserver.Notify(anObject);
        }
    }
}
public class SomeData : ObservableImpl
{
    //被观察者中的数据
    float m_fSomeValue;

    //改变数据的属性
    public float SomeValue
    {
        set
        {
            m_fSomeValue = value;
            base.NotifyObservers(m_fSomeValue);//将改变的消息通知观察者
        }
    }
     
}
//用户界面（观察者）
public class SomeKindOfUI : IObserver
{
    public void Notify(object anObject)
    {
        Console.WriteLine("The new value is:" + anObject);
    }
}

//实际调用的过程
public class MainClass
{
    public static void Main()
    {
        //创建观察者和被观察者
        SomeKindOfUI ui = new SomeKindOfUI();
        SomeData data = new SomeData();

        //在被观察对象中注册观察者
        data.Register(ui);

        //改变被观察对象中的数据，这时被观察者会通知观察者
        data.SomeValue = 1000f;

        //注销观察者，停止观察
       // stock.UnRegister(stockDisplay);
    }
}