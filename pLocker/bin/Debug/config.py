import pyodbc 
mydb = pyodbc.connect(DRIVER='{SQL Server Native Client 11.0}',
Server='192.168.0.5',
UID='sa',
PWD=',rhsm098',
database='mPOS_DB')
mycursor = mydb.cursor()

table1 = 'tbl_employee'
table1_field1 = 'reg_no'
table1_field2 = 'name'

table2 = 'tbl_ein_out_log'
table2_field1 = 'regno'
table2_field2 = 'name'
table2_field3 = 'date'
table2_field4 = 'time_in'
table2_field5 = 'time_out'