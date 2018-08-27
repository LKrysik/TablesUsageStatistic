# T-SQL Tables Usage Statistic for Data Warehouse   [![GitHub license](https://img.shields.io/github/license/LKrysik/TablesUsageStatistic.svg)](https://github.com/LKrysik/TablesUsageStatistic/blob/master/LICENSE)


[![Github Releases](https://img.shields.io/github/downloads/LKrysik/TablesUsageStatistic/latest/total.svg)](https://github.com/LKrysik/TablesUsageStatistic/releases/latest) [![GitHub release](https://img.shields.io/github/release/LKrysik/TablesUsageStatistic.svg)](https://github.com/LKrysik/TablesUsageStatistic/releases) [![Build status](https://tablesusagestatistic.visualstudio.com/TablesUsageStatistic/_apis/build/status/TablesUsageStatistic.svg)](https://tablesusagestatistic.visualstudio.com/TablesUsageStatistic/_apis/build/status/TablesUsageStatistic)


T-SQL Tables Usage Statistic - analyzes trace profiler data or any t-sql queries to find out which tables are in use. 
The tool creates tables usage statistic, with this knowledge you could remove unnecesary tables and facilitate your data warehouse.
This project is published as [code on GitHub](https://github.com/LKrysik/TablesUsageStatistic/).


## What can you do with this tool?

- Insert any SQL (t-sql) queries and find how many times tables and vies ware used. This will help you find unused tables
- Insert SQL directly and create statistic
- Load SQL from file and create statistic
- Load SQL queries from database (incoming)

## What you need

- You need to store some queries executed by end users. You can use Trace Profiler data stored to tables.

## Goal

- For now you can create simple tables usage statistic, this means you can find only tables used by users. In next releases I'm going to implement functionality to list tables doesn't used at all by users.
- Developing any Data Warehouse most of the time you focus on adding new functionality and data, your Data Warehouse is groing there so processing time is growing. Best and simplest way to decrease processing time is to remove unesed data and releated ETL processes.
