#include "Functions.h"

int main()
{
	lists* toDoList{};
	createLists(toDoList);
	loadnames(toDoList);
	bool close = true;

	while (close)
	{
		int choice{};
		cout
			<< "Enter your choice: " << endl
			<< "1. Add " << endl
			<< "2. Show names" << endl
			<< "3. Edit work" << endl
			<< "4. Delete work" << endl
			<< "5. Search work" << endl
			<< "6. Display by execution time" << endl
			<< "7. Sort by priority or execution time: " << endl
			<< "8. Exiting the programm" << endl;
		while (choice < 1 || choice > 8)
			examination(choice);

		switch (choice)
		{
		case 1:
			system("cls");
			toDoList[toDoList->count].Spisok = addList();
			toDoList[toDoList->count].savetoFile();
			toDoList->count++;
			break;
		case 2:
			system("cls");
			for (size_t i = 0; i < toDoList->count; i++)
				toDoList[i].Spisok->print();
			break;
		case 3:
			system("cls");
			editList(toDoList);
			break;
		case 4:
			system("cls");
			deleteList(toDoList);
			break;
		case 5:
			system("cls");
			search(toDoList);
			break;
		case 6:
			system("cls");
			printExecution(toDoList);
			break;
		case 7:
			system("cls");
			sort(toDoList);
			break;
		case 8:
			close = false;
			break;
		}
	}

	return 0;
}
