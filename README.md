# T-SQL Tables Usage Statistic for Data Warehouse  [![Chocolatey](https://img.shields.io/chocolatey/dt/TablesUsageStatistic.svg)](https://chocolatey.org/packages/TablesUsageStatistic/) [![GitHub release](https://img.shields.io/github/release/LKrysik/TablesUsageStatistic.svg)](https://github.com/LKrysik/TablesUsageStatistic/releases) 

The Tables Usage Statistic tools allow you to create tables usage statistic in T-Sql queries. Main goal of this tool is to find what tables was used in queries. Many time you may wonder what tables are used by users and what are not used. With this knowledge you could remove unnecesary tables and remove any existing ETL process.
This project is published as [code on GitHub](https://github.com/LKrysik/TablesUsageStatistic/).


## What can you do with this tool?

- You can connect to database and analyze queries saved by Trace Profiler or you can load queries from file or directly imput to this tool
- Create tables usage statistic - used and unused tables 

## What you need

- You need to store some queries executed by end users. You can use Trace Profiler and save data to tables. 

## Goal

- For now you can create simple tables usage statistic, this means you can find only tables used by users. In next releases I'm going to implement functionality to list tables doesn't used at all by users.
- Developing any Data Warehouse most of the time you focus on adding new functionality and data, your Data Warehouse is groing there so processing time is growing. Best and simplest way to decrease processing time is to remove unesed data and releated ETL processes.
