use healthCenter

	set identity_insert States on
	  insert into States (StateId, stateName) values (1,'Habilitada')
	  insert into States (StateId, stateName) values (2,'Programada')
	  insert into States (StateId, stateName) values (3,'Cancelada')
	set identity_insert States off

