#include "Functions.h"

int length(char* obyekt)
{
	int l{};
	while (obyekt[l] != '\n')
		l++;

	return l;
}

void examination(int& obyekt)
{
	char vvod[101]{};
	cout << "Enter: "; cin >> vvod;

	while ((int)vvod[0] < 47 || (int)vvod[0] > 58)
	{
		cout << "Please enter a number: "; cin >> vvod;
	}

	obyekt = (int)vvod[0] - (int)'0';
	if (((int)vvod[1] > 47 && (int)vvod[1] < 58))
	{
		obyekt *= 10;
		obyekt += (int)vvod[1] - (int)'0';
	}
}

void loadnames(lists*& todolists)
{
	FILE* lists{};
	fopen_s(&lists, "lists.txt", "r");

	if (lists == nullptr) {
		return;
	}

	while (!feof(lists)) {
		todolists[todolists->count].Spisok = new list{};

		fgets(todolists[todolists->count].Spisok->name, 30, lists);
		todolists[todolists->count].Spisok->name[length(todolists[todolists->count].Spisok->name)] = '\0';
		fgets(todolists[todolists->count].Spisok->priority, 30, lists);
		todolists[todolists->count].Spisok->priority[length(todolists[todolists->count].Spisok->priority)] = '\0';
		fgets(todolists[todolists->count].Spisok->description, 1000, lists);
		todolists[todolists->count].Spisok->description[length(todolists[todolists->count].Spisok->description)] = '\0';
		fgets(todolists[todolists->count].Spisok->addDate, 8, lists);
		todolists[todolists->count].Spisok->addDate[length(todolists[todolists->count].Spisok->addDate)] = '\0';
		fgets(todolists[todolists->count].Spisok->executionTime, 8, lists);
		todolists[todolists->count].Spisok->executionTime[length(todolists[todolists->count].Spisok->executionTime)] = '\0';
		todolists->count++;
	}
	todolists->count--;

	fclose(lists);
}

lists* createLists(lists*& todolist)
{
	todolist = new lists{};
	cout << "Enter name: "; cin.getline(todolist->name, 30);

	todolist->Spisok = new list[100]{};

	return todolist;
}

list* addList()
{
	list* l = new list{};
	cin.ignore();
	cout << "Enter name: ";
	cin.getline(l->name, 30);

	while ((int)l->priority[0] < 49 || (int)l->priority[0] > 51 || (int)l->priority[1] > 0)
	{
		cout
			<< "Enter priority: " << endl
			<< "1. Urgent" << endl
			<< "2. Important" << endl
			<< "3. Ordinary" << endl;
		cin.getline(l->priority, 12);
	}

	cout << "Enter description: ";
	cin.getline(l->description, 1000);

	cout << "Enter start date: "; cin.getline(l->addDate, 8);

	while ((int)l->executionTime[0] < 49 || (int)l->executionTime[0] > 51 || (int)l->executionTime[1] > 0)
	{
		cout
			<< "Enter Execution Time: " << endl
			<< "1. Day" << endl
			<< "2. Week" << endl
			<< "3. Month" << endl;

		cin.getline(l->executionTime, 12);
	}

	return l;
}

void editList(lists* todolist)
{
	if (todolist->count == NULL)
		return;

	int choice{};
	cout << "Enter edit list: " << endl;

	for (size_t i = 0; i < todolist->count; i++)
			cout << i + 1 << '.' << todolist[i].Spisok->name << endl;

	while (choice < 1 || choice > todolist->count)
		examination(choice);

	todolist[choice - 1].Spisok = addList();

	FILE* file{};

	fopen_s(&file, "lists.txt", "w");

	if (file == nullptr)
	{
		cout << "Error" << endl;
		return;
	}

	for (size_t i = 0; i < todolist->count; i++)
	{
		fprintf(file, "%s", todolist[i].Spisok->tostring());
	}

	fclose(file);
}

void deleteList(lists* todolist)
{
	if (todolist->count == NULL)
		return;

	int choice{};
	cout << "Enter delete list: " << endl;

	for (size_t i = 0; i < todolist->count; i++)
			cout << i + 1 << '.' << todolist[i].Spisok->name << endl;
	while (choice < 1 || choice > todolist->count)
		examination(choice);

	for (size_t i = choice - 1; i < todolist->count; i++)
	{
		todolist[i].Spisok->name = todolist[i + 1].Spisok->name;
		todolist[i].Spisok->priority = todolist[i + 1].Spisok->priority;
		todolist[i].Spisok->description = todolist[i + 1].Spisok->description;
		todolist[i].Spisok->addDate = todolist[i + 1].Spisok->addDate;
		todolist[i].Spisok->executionTime = todolist[i + 1].Spisok->executionTime;
	}
	todolist->count--;

	FILE* file{};

	fopen_s(&file, "lists.txt", "w");

	if (file == nullptr)
	{
		cout << "Error" << endl;
		return;
	}

	for (size_t i = 0; i < todolist->count; i++)
	{
		fprintf(file, "%s", todolist[i].Spisok->tostring());
	}

	fclose(file);
}

void search(lists* todolist)
{
	char search[1001]{};
	int len{};
	if (todolist->count == NULL)
		return;

	int choice{};
	cout
		<< "Enter choice: " << endl
		<< "1. Search by name" << endl
		<< "2. Search by priority" << endl
		<< "3. Search by description" << endl
		<< "4. Search by Add Date" << endl
		<< "5. Search by Execution date" << endl;
	while (choice < 1 || choice > 5)
		examination(choice);

	getchar();
	cout << "Enter search word: "; cin.getline(search, 1000);

	while (search[len] != '\0')
		len++;

	for (size_t i = 0; i < todolist->count; i++)
	{
		int yes{};
		switch (choice)
		{
		case 1:
			for (size_t j = 0; j < len; j++)
			{
				if (todolist[i].Spisok->name[j] == search[j])
					yes++;
			}
			break;
		case 2:
			for (size_t j = 0; j < len; j++)
			{
				if (todolist[i].Spisok->priority[j] == search[j])
					yes++;
			}
			break;
		case 3:
			for (size_t j = 0; j < len; j++)
			{
				if (todolist[i].Spisok->description[j] == search[j])
					yes++;
			}
			break;
		case 4:
			for (size_t j = 0; j < len; j++)
			{
				if (todolist[i].Spisok->addDate[j] == search[j])
					yes++;
			}
			break;
		case 5:
			for (size_t j = 0; j < len; j++)
			{
				if (todolist[i].Spisok->executionTime[j] == search[j])
					yes++;
			}
			break;
		}
		if (yes == len)
		{
			cout << "Data: " << endl;
			todolist[i].Spisok->print();
		}
	}
}

void printExecution(lists* todolist)
{
	if (todolist->count == NULL)
		return;

	char choice[5]{};
	cout
		<< "Enter choice: " << endl
		<< "1. Day" << endl
		<< "2. Week" << endl
		<< "3. Month" << endl;
	while ((int)choice[0] < 49 || (int)choice[0] > 51 || (int)choice[1] > 0)
		cin.getline(choice, 5);

	for (size_t i = 0; i < todolist->count; i++)
	{
		if ((int)todolist[i].Spisok->executionTime[0] == (int)choice[0])
			cout << todolist[i].Spisok->name << endl;
	}
}

void sort(lists* todolist)
{
	if (todolist->count == NULL)
		return;

	int choice{};
	cout
		<< "How to sort? " << endl
		<< "1. By priority" << endl
		<< "2. By exxecution time" << endl;
	
	while (choice < 1 || choice > todolist->count)
		examination(choice);

	switch (choice)
	{
		case 1:
			for (size_t i = 0; i < todolist->count; i++)
			{
				for (size_t j = 0; j < todolist->count; j++)
				{
					if ((int)todolist[i].Spisok->priority[0] < (int)todolist[j].Spisok->priority[0])
					{
						char* r = todolist[i].Spisok->name;
						todolist[i].Spisok->name = todolist[j].Spisok->name;
						todolist[j].Spisok->name = r;
						r = todolist[i].Spisok->priority;
						todolist[i].Spisok->priority = todolist[j].Spisok->priority;
						todolist[j].Spisok->priority = r;
						r = todolist[i].Spisok->description;
						todolist[i].Spisok->description = todolist[j].Spisok->description;
						todolist[j].Spisok->description = r;
						r = todolist[i].Spisok->addDate;
						todolist[i].Spisok->addDate = todolist[j].Spisok->addDate;
						todolist[j].Spisok->addDate = r;
						r = todolist[i].Spisok->executionTime;
						todolist[i].Spisok->executionTime = todolist[j].Spisok->executionTime;
						todolist[j].Spisok->executionTime = r;
					}
				}
			}
			break;
		case 2:
			for (size_t i = 0; i < todolist->count; i++)
			{
				for (size_t j = 0; j < todolist->count; j++)
				{
					if ((int)todolist[i].Spisok->executionTime[0] < (int)todolist[j].Spisok->executionTime[0])
					{
						char* r = todolist[i].Spisok->name;
						todolist[i].Spisok->name = todolist[j].Spisok->name;
						todolist[j].Spisok->name = r;
						r = todolist[i].Spisok->priority;
						todolist[i].Spisok->priority = todolist[j].Spisok->priority;
						todolist[j].Spisok->priority = r;
						r = todolist[i].Spisok->description;
						todolist[i].Spisok->description = todolist[j].Spisok->description;
						todolist[j].Spisok->description = r;
						r = todolist[i].Spisok->addDate;
						todolist[i].Spisok->addDate = todolist[j].Spisok->addDate;
						todolist[j].Spisok->addDate = r;
						r = todolist[i].Spisok->executionTime;
						todolist[i].Spisok->executionTime = todolist[j].Spisok->executionTime;
						todolist[j].Spisok->executionTime = r;
					}
				}
			}
			break;
	}
}