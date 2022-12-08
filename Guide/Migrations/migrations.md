# How migrations work

We in the nopCommerce team use Migrations to update the database schema when our customers install a newer version of nopCommerce, and the database should be upgraded accordingly. To implement Migrations in nopCommerce, we use the FluentMigrator library.

Let’s look at how we do this.

I would split all nopCommerce classes related to migrations into three categories:

#### 1.   The first category is related to serving all migration processes. These files are general. They are such files from “Nop.Data.Migrations” as 

 -  “MigrationManager” that executes migration;

 - “NopMigrationAttribute” that is used to specify the data of each migration and thus simplify version control for migrations;

 - or some files representing default values related to the migration process.

#### 2. The next category is related to database schema migration. 

It is the “SchemaMigration” class that is placed in the Installation folder. This class contains a list of tables to be created in the database. You can see this class in “Nop.Data” and in the plugins that require adding new tables to the database. Such as the “PickupInStore” plugin that requires a pickup points table or the “Avalara” plugin that creates a tax logging table.

The remarkable point about the schema migration class is that it has the MigrationProcessType set to “Installation”, which means that this migration is only run on the nopCommerce installation process;

#### 3. The last category of migration classes is about migrating data itself. 
Such classes are related to updating tables’ data for a certain nopCommerce version:

  i.   For example, this DataMigration class where we insert new permissions or activity log types;

  ii.  Or the LocalizationMigration class from the Migrations folder of the “Nop.Web.Framework” project. This class works with updating and deleting locale resources;

  iii. Or the SettingMigration class from the same folder that updates settings;
  
  Such classes have the MigrationProcessType set to “Update”, which means that these migrations will not be run during the installation process but only on the next application start.



- This **NopMigration** attribute specified on top of each migration accepts a few parameters. In this case, it’s date and time, migration name, and MigrationProcessType. These parameters are used to sign the current migration. It’s important to set the correct date and time in this attribute. At least, you have to specify the later time for the migration classes created later. Otherwise, it could cause conflicts.

- Other migrations such as **DataMigration** classes accept a nopCommerce version this migration is created for and the UpdateMigrationType parameter. These two parameters are important for migrations that are used in the development process. 

- Let’s open the **MigrationManager** class and see what I mean. There is the ApplyUpMigrations method that applies migrations on the application start. Here you can see that each migration is applied. But! In the debug mode, we don’t save the information about certain migrations, in the MigrationVersionInfo table. They are the migrations that update data, settings, or localization. This means on the next application start these migrations can be applied to the database again.

- It’s very handy because in this case, we can add new data to a migration and can be sure that the new changes will be also applied to the database on the next application start.

