use healthCenter

	set identity_insert States on
	  insert into States (StateId, stateName) values (1,'Available')
	  insert into States (StateId, stateName) values (2,'Scheduled')
	  insert into States (StateId, stateName) values (2,'Canceled')	  
	set identity_insert States off

