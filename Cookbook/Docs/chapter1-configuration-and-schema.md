# Chapter1.The Configuration and Schema

## 1.Installing NHibernate 安装NHibernate
使用nuget图形化安装NHibernate。  
或者在vs的Package Manager Console中`Install-Package NHibernate -Project Eg.Core`。  

## 2.Configuring NHibernate with hibernate.cfg.xml 用hibernate.cfg.xml来配置NHibernate
1. 安装Nhibernate
2. 添加hibernate.cfg.xml到你的项目下。
hibernate.cfg.xml(oracle):  
```xml
<?xml version="1.0" encoding="utf-8"?>
<hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
    <session-factory>
        <property name="connection.driver_class">
            NHibernate.Driver.OracleClientDriver
        </property>
        <property name="dialect">
            NHibernate.Dialect.Oracle10gDialect
        </property>
        <property name="connection.connection_string">
            Data Source=orcl;User ID=dev_test;password=test123
        </property>
        <property name="adonet.batch_size">
            50
        </property>
    </session-factory>
</hibernate-configuration>
```
3. 右键hibernate.cfg.xml-属性:选择Copy if newer
4. 添加如下代码。
```c#
var nhConfig = new Configuration().Configure();
var sessionFactory = nhConfig.BuildSessionFactory();
Console.WriteLine("NHibernate Configured!");
Console.ReadKey();
```
5. 运行你的程序

* 批量处理已经试用于Microsoft SQL Server, Oracle, or MySQL。
* NHibernate默认寻找hibernate.cfg.xml文件。或者你能够用其他方式:
    * 使用不同的文件:
    ```c#
    var cfgFile = "cookbook.cfg.xml"; 
    var nhConfig = new Configuration().Configure(cfgFile);
    ```
    * 使用内嵌的文件:
    ```c#
    var assembly = GetType().Assembly;
    var path = "MyApp.cookbook.cfg.xml"; 
    var nhConfig = new Configuration().Configure(assembly, path);
    ```
    * 使用XmlReader:
    ```c#
    var doc = GetXmlDocumentWithConfig();
    var reader = new XmlNodeReader (doc);
    var nhConfig = new Configuration().Configure(reader);
    ```
* Oracle可用的Dialect:
    * Oracle12cDialect
    * Oracle10gDialect
    * Oracle9iDialect
    * Oracle8iDialect
    * OracleLiteDialect
* Oracle可用的Driver:
    * OracleClientDriver
    * OracleDataClientDriver
    * OracleLiteDataDriver
    * OracleManagedDataClientDriver
    

### NHibernate基础架构
                                                你的应用
                                                   |
配置(Builds session factory)->Session Factory(Builds sessions)->Session(工作单元模式(Unit of Work))
                                                   |
Dialect(方言)(创建sql语法) | Connection Provider(连接提供者)(打开或关闭连接) | Driver(驱动)(创建ADO.NET实体) | Batcher(批处理sql)

## 3.Configuring NHibernate with App.config or Web.config 用App.config或web.config来配置NHibernate
1. 打开App.config
2. 添加如下节点:
    ```xml
    <configSections>
    ? <section name="hibernate-configuration" type="NHibernate.Cfg.ConfigurationSectionHandler, 
    NHibernate" />
    </configSections>
    ```
3. 添加连接字符串:如
    ```xml
    <connectionStrings>
    ? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI"/>
    </connectionStrings>
    ```
4. 添加你的hibernate-configuration节点
    ```xml
    <hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
        <session-factory>
            <property name="connection.driver_class">
                NHibernate.Driver.OracleClientDriver
            </property>
            <property name="dialect">
                NHibernate.Dialect.Oracle10gDialect
            </property>
            <property name="connection.connection_string">
                Data Source=orcl;User ID=dev_test;password=test123
            </property>
            <property name="adonet.batch_size">
                50
            </property>
        </session-factory>
    </hibernate-configuration>
    ```
5. 完整的app.config如下:
    ```xml
    <configuration>
    ? <configSections>
    ??? <section name="hibernate-configuration" type="NHibernate.Cfg.ConfigurationSectionHandler, NHibernate" />
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
6. 添加c#代码:
    ```c#
    var nhConfig = new Configuration().Configure();
    var sessionFactory = nhConfig.BuildSessionFactory();
    Console.WriteLine("NHibernate Configured!");
    Console.ReadKey();    
    ```
7. 运行

* Web.config配置同App.config

## 4.Configuring NHibernate with code 使用代码配置NHibernate
1. 在App.config下面添加如下代码:
    ```xml
    <configuration>
    ? <connectionStrings>
    ??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
    ? </connectionStrings>
    </configuration>
    ```
2. 在Progarm.cs中添加引用:
    ```c#
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    ```
3. 在main方法里添加如下代码:
    ```c#
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
4. 运行。

## 5.Configuring NHibernate with Fluent NHibernate 使用Fluent NHibernate来配置NHibernate(第三方)
第三方库Fluent NHibernate拥有自己的语法来配置NHibernate。  
1. 在app.config文件配置
   ```xml
   <configuration>
   ? <connectionStrings>
   ??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
   ? </connectionStrings>
   </configuration>
   ```
2. 在Program.cs中添加引用:
    ```c#
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    ```
3. 在main方法里添加如下代码:
    ```c#
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
4. 运行


