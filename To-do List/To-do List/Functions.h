#include <iostream>
using namespace std;

struct list
{
	char* name = new char[33] {};
	char* priority = new char[15] {};
	char* description = new char[1003] {};
	char* addDate = new char[10] {};
	char* executionTime = new char[15] {};

	char* tostring()
	{
		char* str = new char[1500] {};
		sprintf_s(str, 1500, "%s\n%s\n%s\n%s\n%s\n", name, priority, description, addDate, executionTime);
		return str;
	}

	void print()
	{
		cout 
			<< "Name: " << name << endl
			<< "Priority: " << priority << endl
			<< "Description: " << description << endl
			<< "Add Date: " << addDate << endl
			<< "Execution Time: " << addDate << endl;
	}
};

struct lists
{
	char* name = new char[31] {};
	list* Spisok{};
	int count{};


	void savetoFile()
	{
		FILE* file{};
		fopen_s(&file, "lists.txt", "a+");

		if (file == nullptr)
		{
			cout << "Error" << endl;
			return;
		}

		fprintf(file, "%s", Spisok->tostring());

		fclose(file);
	}

};

int length(char* obyekt);
void examination(int& obyekt);
void loadnames(lists*& todolists);
lists* createLists(lists*& todolist);
list* addList();
void editList(lists* todolist);
void deleteList(lists* todolist);
void search(lists* todolist);
void printExecution(lists* todolist);
void sort(lists* todolist);