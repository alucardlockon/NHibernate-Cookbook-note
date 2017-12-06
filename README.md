# NHibernate����ָ��
���̳̽����ο���ʾ��Դ��NHibernate 4.x Cookbook - Second Edition�����д�����ָ������Ҳ��NHibernate�˽ⲻ�Ǻ������˵��  

## ʲô��NHibernate
NHibernate��һ������.NET Framework�Ķ����ϵӳ��(ORM,object-relational mapping)��������� ���ṩ��һ�������������ģ�͵���ͳ��ϵ�����ݿ�(rmdbs)��ӳ��Ŀ�ܡ�
NHibernate������˼�壬��ͬNUnit��NAntһ�����ǻ���.Net��Hibernateʵ�֡�

> NHibernate��һ������ġ���Դ��.Net ORM���. ӵ�л�Ծ�Ŀ�����Ⱥ������ȫ�棬���ұ���ǧ���ɹ���������Ŀʹ�á�  
-- ժ�Թ���

## NHibernate��Ϊ������Щ��?
* ��Ҫ����:�ṩ.Net�ൽSQL��������(����ͼ���洢����)��ӳ�䡣
* �ṩ��ѯ:NHibernate�ܸ�����д��ģ�͹�ϵΪ���Զ�����sql��ѯ��䡣
* �����ݿ����:�󲿷�����£����л����ݿⲻ��Ҫ���±�д���������Mysql��Oracle�л����㲻��Ҫ���±�дsql��䡣

## ��Ҫ�汾��Ǩ������
* 1.0(2005)����Hibernate3����ֲ�󲿷ֹ���
* 2.0(2008-4-23): ���Ӽ����ԣ�����֧��.Net1.1��
* 3.0(2010-12-4)������Linq֧�֣�����QueryOver֧�֣��µ�HQL���棬֧���������С�ʹ��.Net 3.5
* 3.2(2011-4): ����Mapping By Code���ܡ�����SubSelect(�Ӳ�ѯӳ��)��
* 4.0(2014-4-17): ��Linq���õ�֧�֡�
* 5.0(2017-10-10): ���첽����ṩ֧�֡�

## �̳̼��ο�
* NHibernate�ٷ���վ: http://nhibernate.info/  
* NHibernate�ٷ������: https://github.com/nhibernate/nhibernate-core  
* NHibernate֮��ϵ������(�����ģ����ǰ汾�����ˣ��������ţ��ܶ�ط�������ȫ��): http://www.cnblogs.com/lyj/archive/2008/10/30/1323099.html  
* NHibernate 4.x Cookbook - Second Edition(NHibernate4.xָ�ϵڶ��棬��������Ӽ�����Դ�ڴ���): https://www.amazon.com/gp/product/1784396427

## HelloWorldʾ��
1. ��Nuget��װNHibernate����Ŀ���̨��Ŀ�����á�������PM�д���`Install-Package NHibernate -Project �����Ŀ��`
2. ���һ������`TestClass`��class��
  ```csharp
  public class TestClass
  {
  ? public virtual int Id { get; set; }
  ? public virtual string Name { get; set; }
  }
  ```
3. ��������,����Ŀ�����`hibernate.cfg.xml`�ļ������ļ���Ҫ��ѡ���дӲ����Ƹ�Ϊ���Ǹ��ƻ������������:
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
4. �����`Program.cs`�е�`main`�����¼������д���:
  ```csharp
  var nhConfig = new Configuration().Configure();
  var sessionFactory = nhConfig.BuildSessionFactory();
  Console.WriteLine("Hello,World!NHibernate ���óɹ���!");
  ```
5. ������Ŀ���̨���򣬲鿴�����

## ����ƪ
### NHibernate��������(<small><small>�����м������õ�</small></small>):
* connection.provider
* connection.connection_string
* dialect
* command_timeout
* show_sql/format_sql
* adonet.batch_size

#### ֧�ֵ�Dialect
* **SqlServer**:MsSql2012Dialect/MsSql2008Dialect��
* **Oracle**:Oracle12cDialect/Oracle10gDialect/Oracle9iDialect��
* **MySql**:MySQL55Dialect/MySQL55InnoDBDialect/MySQLDialect��
* **PostgreSQL**:PostgreSQLDialect��
* **DB2**:DB2Dialect��
* **SQLite**:SQLiteDialect
* **Sybase**:SybaseASA9Dialect��
* **Informix**:InformixDialect��
* **Firebird**:FirebirdDialect
* **Ingres**:IngresDialect/Ingres9Dialect

### ʹ��XML����
NHibernate����������÷�����֮ǰ��Hello World�Ѿ���ʾ����XML���÷�����  
NHibernateĬ�ϻ��Զ���`hibernate.cfg.xml`�ļ���  
* ������뻻���ļ���ʹ���������:
  ```csharp
  var cfgFile = "cookbook.cfg.xml";
  var nhConfig = new Configuration().Configure(cfgFile);
  ```
* �����������ļ���Ƕ���:
  ```csharp
  var assembly = GetType().Assembly;
  var path = "MyApp.cookbook.cfg.xml";
  var nhConfig = new Configuration().Configure(assembly, path);
  ```
* ʹ��xmlReader��:
  ```csharp
  var doc = GetXmlDocumentWithConfig();
  var reader = new XmlNodeReader (doc);
  var nhConfig = new Configuration().Configure(reader);
  ```

### ��App/Web.Config����
�����app/web.configӦ�����ó���������:
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

### ����Ĵ���������
1. �����app/web.config�¼��������ַ���:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
? <connectionStrings>
??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
? </connectionStrings>
</configuration>
```
2. �����`main`�����������������:
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
3. ���С�(PS.��Ȼ�����Ҳ��`db.ConnectionString = @"xxx"`��������������ַ��������ǲ�û�˻���ô��..)

### Fluent NHibernate(��ϸ��google)
1. ʹ��Fluent NHibernate(PS.��Ҫ��Nuget��װFNH��)(PS2.���������û������,�����ǿ����õģ����Ҳ�û���о���)
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

## ӳ��ƪ
��ͷϷ����ģ�͵�ӳ�����orm������Ҫ��
### ��XMLӳ����
1. ��һ���������һ��guid:
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
2. ��һ�����ڻ����`Product`��:
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
3. ��һ��`Product.hbm.xml`��,��������ΪǶ�����Դ:
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
4. �������´���:
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

#### Id��generator
* guid
* guid.comb
* guid.native
* uuid.hex
* uuid.string
* increment
* sequence

#### һ�Զ�ӳ��
1.������������`Movie`��`ActorRole`:
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
2. ��movie��ӳ��������������������һ�Զ�ӳ��
  ```xml
  <list name="Actors" cascade="all-delete-orphan">
??? <key column="MovieId" />
??? <index column="ActorIndex" />
??? <one-to-many class="ActorRole"/>
? </list>
  ```
3. �������д���:
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

#### ������
�������һ�Զ�ʾ��:�����ǻ�ȡ`Movie`��ʱ��`Movie`�����`Actors`��ֻ��id����ֵ�ģ�������ʹ����`ActorRold`��������ֶΣ�NHibernate�Ż�ȥ���������ֶΡ�  

#### NHibernate�м��ϵ�����
* **Bag**:IList<T>�������ظ�������
  ```xml
  <bag name="Actors">
  ? <key column="MovieId"/>
  ? <one-to-many class="ActorRole"/>
  </bag>
  ```
* **Set**:ISet<T>�������ظ�������
  ```xml
  <set name="Actors">
  ? <key column="MovieId" />
  ? <one-to-many class="ActorRole"/>
  </set>
  ```
* **List**:IList<T>,�����ظ�������
  ```xml
  <list name="Actors">
  ? <key column="MovieId" />
  ? <list-index column="ActorRoleIndex" />
  ? <one-to-many class="ActorRole"/>
  </list>
  ```
* **Map**:IDictionary<TKey,TValue>,KeyΨһ������
  ```xml
  <map name="Actors" >
  ? <key column="MovieId" />
  ? <map-key column="Role" type="string" />
  ? <element column="Actor" type="string"/>
  </map>
  ```

#### ��Զ�ӳ��
1. �½�һ��`Student`��:
  ```csharp
  public class Student
  {
  ??? public virtual Guid Id { get; protected set; }
  ??? public virtual string Name { get; set; }
  }
  ```
2. ����`Student.hbm.xml`:
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
3. �½�`Course`��:
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
4. ����`Course.hbm.xml`��:
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
5. ����:
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

### ��NHibernate.Mapping.Attributes�Զ�����xml
��ģ������ʹ��NHibernate.Mapping.Attributes�������ռ��ע�⣬�ܴﵽ�Զ�����xml��Ŀ�ġ�
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

### �ô���ӳ��
1. ����һ��`ProductMapping`��:
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
2. �����������:
  ```csharp
  var mapper = new ModelMapper();
  mapper.AddMapping<ProductMapping>();
  var mapping =
mapper.CompileMappingForAllExplicitlyAddedEntities();
??cfg.AddMapping(mapping);
  ```

#### ��Լ����ӳ��
�ɼ̳�`ConventionModelMapper`�����ﵽ�Զ�������ģ�͵�Ŀ�ġ�  

## ����ƪ
ʹ��`session.BeginTransaction()`��ʼ����Ȼ�����`Commit�ύ`  
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

## ��ѯƪ
### ֱ�Ӳ�ѯId
```csharp
// ��ѯidΪ1��Product
var product1 = session.Get<Product>(1);
```

### ��Linq to NHibernate��ѯ(3.0��ʼ֧��)
```csharp
var book = session.Query<Book>()
??????? .FirstOrDefault(x => x.ISBN == isbn);
var moive = session.Query<Movie>()
??????? .Where(x => x.Director == directorName)
??????? .ToList();
```

### ��Criteria API��ѯ
```csharp
var book = session.CreateCriteria<Book>()
????????????? .Add(Restrictions.Eq("ISBN", isbn))
????????????? .UniqueResult<Book>();
var movie = session.CreateCriteria<Movie>()
????????????? .Add(Restrictions.Eq("Director",
?????????????? directorName))
????????????? .List<Movie>();
```

### ��QueryOver API��ѯ(3.0��ʼ)
QueryOver��ѯ�﷨����linq���﷨������������Linq��
```csharp
var book = session.QueryOver<Book>()
??? .Where(b => b.ISBN == isbn)
??? .SingleOrDefault();
var movie = session.QueryOver<Movie>()
??? .Where(m => m.Director == directorName)
??? .List();
```

### ��Hibernate Query Language(HQL)��ѯ
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

### ��SQL��ѯ
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

### Linq��������
```csharp
var book = session.Query<Book>()
??????????????? .Fetch(x => x.Publisher)
??????????????? .FirstOrDefault();
var movie = session.Query<Movie>()
??????????????? .FetchMany(x => x.Actors)
??????????????? .ToList();
```

### ����Sql
1. ���`Queries.hbm.xml`:
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
3. ʹ������sql:
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

# ����
���Ͼ���NHibernate���µĳ���ָ���ˡ�
