# NHibernate初步指南
本教程仅供参考，示例源自NHibernate 4.x Cookbook - Second Edition。如有错误请指正，我也对NHibernate了解不是很深入的说。  

## 什么是NHibernate
NHibernate是一个面向.NET Framework的对象关系映射(ORM,object-relational mapping)解决方案。 它提供了一个面向对象领域模型到传统关系型数据库(rmdbs)的映射的框架。
NHibernate，顾名思义，如同NUnit，NAnt一样，是基于.Net的Hibernate实现。

> NHibernate是一个成熟的、开源的.Net ORM框架. 拥有活跃的开发社群、功能全面，并且被数千个成功的优秀项目使用。  
-- 摘自官网

## NHibernate能为你做哪些事?
* 主要功能:提供.Net类到SQL数据类型(表，试图，存储过程)的映射。
* 提供查询:NHibernate能根据你写的模型关系为你自动生成sql查询语句。
* 与数据库独立:大部分情况下，你切换数据库不需要重新编写程序，例如从Mysql到Oracle切换，你不需要重新编写sql语句。

## 主要版本变迁及功能
* 1.0(2005)：从Hibernate3中移植大部分功能
* 2.0(2008-4-23): 增加兼容性，不再支持.Net1.1。
* 3.0(2010-12-4)：增加Linq支持，增加QueryOver支持，新的HQL引擎，支持懒加载列。使用.Net 3.5
* 3.2(2011-4): 增加Mapping By Code功能。增加SubSelect(子查询映射)。
* 4.0(2014-4-17): 对Linq更好的支持。
* 5.0(2017-10-10): 对异步编程提供支持。

## 教程及参考
* NHibernate官方网站: http://nhibernate.info/  
* NHibernate官方代码库: https://github.com/nhibernate/nhibernate-core  
* NHibernate之旅系列文章(是中文，但是版本较老了，仅供入门，很多地方还不够全面): http://www.cnblogs.com/lyj/archive/2008/10/30/1323099.html  
* NHibernate 4.x Cookbook - Second Edition(NHibernate4.x指南第二版，下面的例子几乎都源于此书): https://www.amazon.com/gp/product/1784396427

## HelloWorld示例
1. 打开Nuget安装NHibernate到你的控制台项目并配置。或者在PM中打下`Install-Package NHibernate -Project 你的项目名`
2. 添加一个名叫`TestClass`的class。
  ```csharp
  public class TestClass
  {
  ? public virtual int Id { get; set; }
  ? public virtual string Name { get; set; }
  }
  ```
3. 进行配置,在项目中添加`hibernate.cfg.xml`文件，此文件需要在选项中从不复制改为总是复制或如果较新则复制:
  ```xml
  <?xml version="1.0" encoding="utf-8"?>
  <hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
  ? <session-factory>
  ??? <property name="dialect">
  ????? NHibernate.Dialect.MsSql2012Dialect, NHibernate
  ??? </property>
  ??? <property name="connection.connection_string">
  ????? Server=.\SQLEXPRESS; Database=NHCookbook;
  ????? Trusted_Connection=SSPI
  ??? </property>
  ??? <property name="adonet.batch_size">
  ????? 100
  ??? </property>
  ? </session-factory>
  </hibernate-configuration>  
  ```
4. 在你的`Program.cs`中的`main`方法下加入下列代码:
  ```csharp
  var nhConfig = new Configuration().Configure();
  var sessionFactory = nhConfig.BuildSessionFactory();
  Console.WriteLine("Hello,World!NHibernate 配置成功了!");
  ```
5. 运行你的控制台程序，查看输出。

## 配置篇
### NHibernate配置属性(<small><small>这里列几个常用的</small></small>):
* connection.provider
* connection.connection_string
* dialect
* command_timeout
* show_sql/format_sql
* adonet.batch_size

#### 支持的Dialect
* **SqlServer**:MsSql2012Dialect/MsSql2008Dialect等
* **Oracle**:Oracle12cDialect/Oracle10gDialect/Oracle9iDialect等
* **MySql**:MySQL55Dialect/MySQL55InnoDBDialect/MySQLDialect等
* **PostgreSQL**:PostgreSQLDialect等
* **DB2**:DB2Dialect等
* **SQLite**:SQLiteDialect
* **Sybase**:SybaseASA9Dialect等
* **Informix**:InformixDialect等
* **Firebird**:FirebirdDialect
* **Ingres**:IngresDialect/Ingres9Dialect

### 使用XML配置
NHibernate最基础的配置方法，之前的Hello World已经演示过了XML配置方法。  
NHibernate默认会自动找`hibernate.cfg.xml`文件。  
* 如果你想换个文件名使用下面代码:
  ```csharp
  var cfgFile = "cookbook.cfg.xml";
  var nhConfig = new Configuration().Configure(cfgFile);
  ```
* 如果你的配置文件是嵌入的:
  ```csharp
  var assembly = GetType().Assembly;
  var path = "MyApp.cookbook.cfg.xml";
  var nhConfig = new Configuration().Configure(assembly, path);
  ```
* 使用xmlReader类:
  ```csharp
  var doc = GetXmlDocumentWithConfig();
  var reader = new XmlNodeReader (doc);
  var nhConfig = new Configuration().Configure(reader);
  ```

### 用App/Web.Config配置
在你的app/web.config应该配置成如下这样:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
? <configSections>
??? <section name="hibernate-configuration" type="NHibernate.Cfg.ConfigurationSectionHandler,
????? NHibernate" />
? </configSections>
? <connectionStrings>
??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
? </connectionStrings>
<hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
? <session-factory>
??? <property name="dialect">
????? NHibernate.Dialect.MsSql2008Dialect, NHibernate
??? </property>
??? <property name="connection.connection_string_name">
????? db
??? </property>
??? <property name="adonet.batch_size">
????? 100
??? </property>
? </session-factory>
</hibernate-configuration>
</configuration>
```

### 用你的代码来配置
1. 在你的app/web.config下加上连接字符串:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
? <connectionStrings>
??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
? </connectionStrings>
</configuration>
```
2. 在你的`main`方法里增加下面代码:
```csharp
var nhConfig = new Configuration().DataBaseIntegration(db =>
{
? db.Dialect<MsSql2012Dialect>();
? db.ConnectionStringName = "db";
? db.BatchSize = 100;
});
var sessionFactory = nhConfig.BuildSessionFactory();
Console.WriteLine("NHibernate Configured!");
Console.ReadKey();
```
3. 运行。(PS.当然你可以也用`db.ConnectionString = @"xxx"`来配置你的连接字符串，但是并没人会这么做..)

### Fluent NHibernate(详细请google)
1. 使用Fluent NHibernate(PS.需要用Nuget安装FNH包)(PS2.这货近两年没更新了,但还是可以用的，但我并没有研究过)
  ```csharp
  using FluentNHibernate.Cfg;
  using FluentNHibernate.Cfg.Db;
  ```
  ```csharp
  var config = MsSqlConfiguration.MsSql2012
  ? .ConnectionString(connstr => connstr.FromConnectionStringWithKey("db"))
  ? .AdoNetBatchSize(100);
  var nhConfig = Fluently.Configure()
  ? .Database(config)
  ? .BuildConfiguration();
  var sessionFactory = nhConfig.BuildSessionFactory();
  Console.WriteLine("NHibernate configured fluently!");
  Console.ReadKey();
  ```

## 映射篇
重头戏来了模型的映射对于orm至关重要。
### 用XML映射类
1. 建一个基类包含一个guid:
  ```csharp
  using System;
  namespace Eg.Core
  {
  ? public abstract class Entity
  ? {
  ??? public virtual Guid Id { get; protected set; }
  ? }
  }
  ```
2. 建一个基于基类的`Product`类:
  ```csharp
  using System;
  namespace Eg.Core
  {
  ? public class Product : Entity
  ? {
  ??? public virtual string Name { get; set; }
  ??? public virtual string Description { get; set; }
  ??? public virtual decimal UnitPrice { get; set; }
    }
  }
  ```
3. 建一个`Product.hbm.xml`类,设置属性为嵌入的资源:
  ```xml
  <?xml version="1.0" encoding="utf-8" ?>
  <hibernate-mapping xmlns="urn:nhibernate-mapping-2.2"
  ??? assembly="Eg.Core"
  ??? namespace="Eg.Core">
  ? <class name="Product">
  ??? <id name="Id">
  ????? <generator class="guid.comb" />
  ??? </id>
  ??? <property name="Name" not-null="true" />
  ??? <property name="Description" />
  ??? <property name="UnitPrice" not-null="true"
  ????? type="Currency" />
  ? </class>
  </hibernate-mapping>
  ```
4. 试试以下代码:
  ```csahrp
  cfg.AddAssembly(typeof(Product).Assembly);
  ```
  ```csharp
  session.Save(new Product
????{
????? Name = "Car",
????? Description = "A nice red car",
????? UnitPrice = 300
???? });
  ```

#### Id的generator
* guid
* guid.comb
* guid.native
* uuid.hex
* uuid.string
* increment
* sequence

#### 一对多映射
1.现在有两个类`Movie`和`ActorRole`:
  ```csharp
  public class ActorRole : Entity
  {
  ??public virtual string Actor { get; set; }
  ??public virtual string Role { get; set; }
  }
  public class Movie : Product
  {
  ??public virtual string Director { get; set; }
  ??public virtual IList<ActorRole> Actors { get; set; }
  }
  ```
2. 在movie的映射里配置如下内容配置一对多映射
  ```xml
  <list name="Actors" cascade="all-delete-orphan">
??? <key column="MovieId" />
??? <index column="ActorIndex" />
??? <one-to-many class="ActorRole"/>
? </list>
  ```
3. 试试下列代码:
  ```csharp
  session.Save(new Movie
??? {
??????? Name = "Hibernation",
??????? Description =
????????? "The countdown for the lift-off has begun",
??????? UnitPrice = 300,
??????? Actors=new List<ActorRole>
??????? {
??????????? new ActorRole
??????????? {
??????????????? Actor = "Adam Quintero",
??????????????? Role = "Joseph Wood"
??????????? }
??????? }
??? });
  ```

#### 懒加载
如上面的一对多示例:当我们获取`Movie`的时候，`Movie`对象的`Actors`里只有id是有值的，当我们使用了`ActorRold`里的其他字段，NHibernate才会去加载其他字段。  

#### NHibernate中集合的类型
* **Bag**:IList<T>，允许重复，无序
  ```xml
  <bag name="Actors">
  ? <key column="MovieId"/>
  ? <one-to-many class="ActorRole"/>
  </bag>
  ```
* **Set**:ISet<T>不允许重复，无序
  ```xml
  <set name="Actors">
  ? <key column="MovieId" />
  ? <one-to-many class="ActorRole"/>
  </set>
  ```
* **List**:IList<T>,允许重复，有序
  ```xml
  <list name="Actors">
  ? <key column="MovieId" />
  ? <list-index column="ActorRoleIndex" />
  ? <one-to-many class="ActorRole"/>
  </list>
  ```
* **Map**:IDictionary<TKey,TValue>,Key唯一，无序
  ```xml
  <map name="Actors" >
  ? <key column="MovieId" />
  ? <map-key column="Role" type="string" />
  ? <element column="Actor" type="string"/>
  </map>
  ```

#### 多对多映射
1. 新建一个`Student`类:
  ```csharp
  public class Student
  {
  ??? public virtual Guid Id { get; protected set; }
  ??? public virtual string Name { get; set; }
  }
  ```
2. 配置`Student.hbm.xml`:
  ```xml
  <?xml version="1.0" encoding="utf-8" ?>
  <hibernate-mapping xmlns="urn:nhibernate-mapping-2.2"
  ??? assembly="MappingRecipes"
  ??? namespace="MappingRecipes.ManyToMany">
  ? <class name="Student">
  ??? <id name="Id">
  ????? <generator class="guid.comb"/>
  ??? </id>
  ??? <property name="Name"/>
  ? </class>
  </hibernate-mapping>
  ```
3. 新建`Course`类:
  ```csharp
  public class Course
  {
  ??? public Course()
  ??? {
        Students=new HashSet<Student>();
  ??? }
  ??? public virtual Guid Id { get; protected set; }
  ??? public virtual string Name { get; set; }
  ??? public virtual ISet<Student> Students { get; set; }
  }
  ```
4. 配置`Course.hbm.xml`类:
  ```xml
  <?xml version="1.0" encoding="utf-8" ?>
  <hibernate-mapping xmlns="urn:nhibernate-mapping-2.2"
  ??? assembly="MappingRecipes"
  ??? namespace="MappingRecipes.ManyToMany">
  ? <class name="Course">
  ??? <id name="Id">
  ????? <generator class="guid.comb"/>
  ??? </id>
  ??? <property name="Name"/>
  ??? <set name="Students" table="CourseStudent">
  ????? <key column="CourseId"/>
  ????? <many-to-many class="Student" column="StudentId"/>
  ??? </set>
  ? </class>?
  </hibernate-mapping>
  ```
5. 试试:
  ```csharp
  var anna = new Student { Name = "Anna" };
??var george = new Student { Name = "George" };
  var english = new Course { Name = "English" };
??var french = new Course { Name = "French" };

??english.Students.Add(anna);

??french.Students.Add(anna);
??french.Students.Add(george);

??session.Save(anna);
??session.Save(george);

??session.Save(english);
??session.Save(french);

??_frenchId = french.Id;

  var course2 = session.Get<Course>(_frenchId);
??Console.WriteLine("Course name: " + course2.Name);
??Console.WriteLine("Student count: " +
??course2.Students.Count());
  ```

### 用NHibernate.Mapping.Attributes自动生成xml
在模型类中使用NHibernate.Mapping.Attributes的命名空间的注解，能达到自动生成xml的目的。
```csharp
[Id(0, Name="Id",TypeType=typeof (int),Column="Id",UnsavedValue="0")]  
[Key(1)]
[Generator(2, Class="native")]
public int Id  
{
  get { return _id; }
  set { _id = value; }
}
[PropertyAttribute(Column="PName",Length=50,TypeType=typeof(String))]
public string PName  
{
  get { return _pName; }
  set { _pName = value; }
}
```

### 用代码映射
1. 创建一个`ProductMapping`类:
  ```csharp
  using NH4CookbookHelpers.Mapping.Model;
  using NHibernate.Mapping.ByCode;
  using NHibernate.Mapping.ByCode.Conformist;

  namespace MappingRecipes.MappingByCode
  {
  public class ProductMapping : ClassMapping<Product>
  {
  ? public ProductMapping()
  ? {
  ?? Table("Product");
  ?? Id(x => x.Id, x => x.Generator(Generators.GuidComb));
  ?? Property(p => p.Name);
  ?? Property(p => p.Description);
  ?? Property(p => p.UnitPrice);
  ? }
  }
  }
  ```
2. 加上下面代码:
  ```csharp
  var mapper = new ModelMapper();
  mapper.AddMapping<ProductMapping>();
  var mapping =
mapper.CompileMappingForAllExplicitlyAddedEntities();
??cfg.AddMapping(mapping);
  ```

#### 用约定来映射
可继承`ConventionModelMapper`类来达到自动化配置模型的目的。  

## 事务篇
使用`session.BeginTransaction()`开始事务，然后调用`Commit提交`  
```csharp
var session = _sessionProvider.GetCurrentSession();
IEnumerable<TEntity> results;
using (var tx = session.BeginTransaction())
{
???results = session.QueryOver<TEntity>()
?????.List<TEntity>();
???tx.Commit();
?}
```

## 查询篇
### 直接查询Id
```csharp
// 查询id为1的Product
var product1 = session.Get<Product>(1);
```

### 用Linq to NHibernate查询(3.0开始支持)
```csharp
var book = session.Query<Book>()
??????? .FirstOrDefault(x => x.ISBN == isbn);
var moive = session.Query<Movie>()
??????? .Where(x => x.Director == directorName)
??????? .ToList();
```

### 用Criteria API查询
```csharp
var book = session.CreateCriteria<Book>()
????????????? .Add(Restrictions.Eq("ISBN", isbn))
????????????? .UniqueResult<Book>();
var movie = session.CreateCriteria<Movie>()
????????????? .Add(Restrictions.Eq("Director",
?????????????? directorName))
????????????? .List<Movie>();
```

### 用QueryOver API查询(3.0开始)
QueryOver查询语法类似linq的语法，但他并不是Linq。
```csharp
var book = session.QueryOver<Book>()
??? .Where(b => b.ISBN == isbn)
??? .SingleOrDefault();
var movie = session.QueryOver<Movie>()
??? .Where(m => m.Director == directorName)
??? .List();
```

### 用Hibernate Query Language(HQL)查询
```csharp
var hql = @"from Book b
??????? where b.ISBN = :isbn";
var book = _session.CreateQuery(hql)
??????? .SetString("isbn", isbn)
??????? .UniqueResult<Book>();
var hql2 = @"from Movie m
??????? where m.Director = :director";
var movie = session.CreateQuery(hql2)
??????? .SetString("director", directorName)
??????? .List<Movie>();
```

### 用SQL查询
```csharp
var sql = @"select b.* from Product b
??????? where b.ISBN = :isbn";
var book = _session.CreateSQLQuery(sql)
??????? .AddEntity(typeof(Book))
??????? .SetString("isbn", isbn)
??????? .UniqueResult<Book>();
var sql2 = @"select * from Product?
??????? where ProductType = 'Movie'
??????? and Director = :director";
var movie = _session.CreateSQLQuery(sql2)
??????? .AddEntity(typeof(Movie))
??????? .SetString("director", directorName)
??????? .List<Movie>();
```

### Linq立即加载
```csharp
var book = session.Query<Book>()
??????????????? .Fetch(x => x.Publisher)
??????????????? .FirstOrDefault();
var movie = session.Query<Movie>()
??????????????? .FetchMany(x => x.Actors)
??????????????? .ToList();
```

### 命名Sql
1. 添加`Queries.hbm.xml`:
  ```xml
  <?xml version="1.0" encoding="utf-8" ?>
  <hibernate-mapping xmlns="urn:nhibernate-mapping-2.2">
  ? <query name="GetBookByISBN">
  ??? <![CDATA[
  ??? from Book b where b.ISBN = :isbn
  ??? ]]>
  ? </query>
  </hibernate-mapping>
  ```
3. 使用命名sql:
  ```csharp
  config.AddResource(
    "QueryRecipes.NamedQueries.Queries.hbm.xml",
    GetType().Assembly);
  ```
  ```csharp
  session.GetNamedQuery("GetBookByISBN")
????.SetString("isbn", isbn)
????.UniqueResult<Book>();
  ```

# 结束
以上就是NHibernate大致的初步指南了。
