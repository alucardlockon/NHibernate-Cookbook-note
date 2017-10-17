# Chapter1.The Configuration and Schema

## 1.Installing NHibernate ��װNHibernate
ʹ��nugetͼ�λ���װNHibernate��  
������vs��Package Manager Console��`Install-Package NHibernate -Project Eg.Core`��  

## 2.Configuring NHibernate with hibernate.cfg.xml ��hibernate.cfg.xml������NHibernate
1. ��װNhibernate
2. ���hibernate.cfg.xml�������Ŀ�¡�
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
3. �Ҽ�hibernate.cfg.xml-����:ѡ��Copy if newer
4. ������´��롣
```c#
var nhConfig = new Configuration().Configure();
var sessionFactory = nhConfig.BuildSessionFactory();
Console.WriteLine("NHibernate Configured!");
Console.ReadKey();
```
5. ������ĳ���

* ���������Ѿ�������Microsoft SQL Server, Oracle, or MySQL��
* NHibernateĬ��Ѱ��hibernate.cfg.xml�ļ����������ܹ���������ʽ:
    * ʹ�ò�ͬ���ļ�:
    ```c#
    var cfgFile = "cookbook.cfg.xml"; 
    var nhConfig = new Configuration().Configure(cfgFile);
    ```
    * ʹ����Ƕ���ļ�:
    ```c#
    var assembly = GetType().Assembly;
    var path = "MyApp.cookbook.cfg.xml"; 
    var nhConfig = new Configuration().Configure(assembly, path);
    ```
    * ʹ��XmlReader:
    ```c#
    var doc = GetXmlDocumentWithConfig();
    var reader = new XmlNodeReader (doc);
    var nhConfig = new Configuration().Configure(reader);
    ```
* Oracle���õ�Dialect:
    * Oracle12cDialect
    * Oracle10gDialect
    * Oracle9iDialect
    * Oracle8iDialect
    * OracleLiteDialect
* Oracle���õ�Driver:
    * OracleClientDriver
    * OracleDataClientDriver
    * OracleLiteDataDriver
    * OracleManagedDataClientDriver
    

### NHibernate�����ܹ�
                                                ���Ӧ��
                                                   |
����(Builds session factory)->Session Factory(Builds sessions)->Session(������Ԫģʽ(Unit of Work))
                                                   |
Dialect(����)(����sql�﷨) | Connection Provider(�����ṩ��)(�򿪻�ر�����) | Driver(����)(����ADO.NETʵ��) | Batcher(������sql)

## 3.Configuring NHibernate with App.config or Web.config ��App.config��web.config������NHibernate
1. ��App.config
2. ������½ڵ�:
    ```xml
    <configSections>
    ? <section name="hibernate-configuration" type="NHibernate.Cfg.ConfigurationSectionHandler, 
    NHibernate" />
    </configSections>
    ```
3. ��������ַ���:��
    ```xml
    <connectionStrings>
    ? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI"/>
    </connectionStrings>
    ```
4. ������hibernate-configuration�ڵ�
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
5. ������app.config����:
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
6. ���c#����:
    ```c#
    var nhConfig = new Configuration().Configure();
    var sessionFactory = nhConfig.BuildSessionFactory();
    Console.WriteLine("NHibernate Configured!");
    Console.ReadKey();    
    ```
7. ����

* Web.config����ͬApp.config

## 4.Configuring NHibernate with code ʹ�ô�������NHibernate
1. ��App.config����������´���:
    ```xml
    <configuration>
    ? <connectionStrings>
    ??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
    ? </connectionStrings>
    </configuration>
    ```
2. ��Progarm.cs���������:
    ```c#
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    ```
3. ��main������������´���:
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
4. ���С�

## 5.Configuring NHibernate with Fluent NHibernate ʹ��Fluent NHibernate������NHibernate(������)
��������Fluent NHibernateӵ���Լ����﷨������NHibernate��  
1. ��app.config�ļ�����
   ```xml
   <configuration>
   ? <connectionStrings>
   ??? <add name="db" connectionString="Server=.\SQLEXPRESS; Database=NHCookbook; Trusted_Connection=SSPI" />
   ? </connectionStrings>
   </configuration>
   ```
2. ��Program.cs���������:
    ```c#
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    ```
3. ��main������������´���:
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
4. ����


