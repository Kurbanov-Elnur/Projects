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

songs* createSongs(songs*& Songs)
{
	Songs = new songs{};
	cout << "Enter name: "; cin.getline(Songs->name, 30);

	Songs->Song = new song[100]{};

	return Songs;
}

char* loadSongs(songs*& Songs)
{
	FILE* songs{};
	fopen_s(&songs, "songs.txt", "r");

	if (songs == nullptr) {
		return nullptr;
	}

	while (!feof(songs)) {
		Songs[Songs->count].Song = new song{};

		fgets(Songs[Songs->count].Song->name, 30, songs);
		Songs[Songs->count].Song->name[length(Songs[Songs->count].Song->name)] = '\0';
		fgets(Songs[Songs->count].Song->author, 30, songs);
		Songs[Songs->count].Song->author[length(Songs[Songs->count].Song->author)] = '\0';
		fgets(Songs[Songs->count].Song->text, 1000, songs);
		Songs[Songs->count].Song->text[length(Songs[Songs->count].Song->text)] = '\0';
		fgets(Songs[Songs->count].Song->year, 5, songs);
		Songs[Songs->count].Song->year[length(Songs[Songs->count].Song->year)] = '\0';
		Songs->count++;
	}
	Songs->count--;

	fclose(songs);
}

song* addSong()
{
	song* s = new song{};
	cin.ignore();
	cout << "Enter name: ";
	cin.getline(s->name, 30);

	cout << "Enter author: ";
	cin.getline(s->author, 30);

	int c{};
	cout
		<< "1. Manual input" << endl
		<< "2. Loading from a file" << endl;
	while (c < 1 || c > 2)
		examination(c);
	cin.ignore();
	if (c == 1)
	{
		cout << "Enter text: ";
		cin.getline(s->text, 1000);
	}
	else if (c == 2)
	{
		char* filename = new char[30] {};
		char* fileName = new char[34] {};
		char* extension = new char[] {".txt"};

		cout << "Enter file name: ";
		cin.getline(filename, 30);

		int i = 0;
		while (filename[i] != '\0') {
			fileName[i] = filename[i];
			i++;
		}

		int j = 0;
		while (extension[j] != '\0') {
			fileName[i] = extension[j];
			i++;
			j++;
		}

		delete[] filename;

		FILE* file{};
		fopen_s(&file, fileName, "r");

		if (file == nullptr) {
			cout << "Error" << endl;
			return nullptr;
		}

		while (!feof(file)) {
			fgets(s->text, 1000, file);
		}

		fclose(file);
	}

	cout << "Enter year: "; cin.getline(s->year, 5);

	return s;
}

void deleteSong(songs* Songs)
{
	if (Songs->count == NULL)
		return;

	int choice{};
	cout << "Enter delete song: " << endl;

	for (size_t i = 0; i < Songs->count; i++)
		cout << i + 1 << '.' << Songs[i].Song->name << endl;
	while (choice < 1 || choice > Songs->count)
		examination(choice);
	
	for (size_t i = choice - 1; i < Songs->count; i++)
	{
		Songs[i].Song->name = Songs[i + 1].Song->name;
		Songs[i].Song->author = Songs[i + 1].Song->author;
		Songs[i].Song->text = Songs[i + 1].Song->text;
		Songs[i].Song->year = Songs[i + 1].Song->year;
	}
	Songs->count--;

	FILE* file{};

	fopen_s(&file, "songs.txt", "w");

	if (file == nullptr)
	{
		cout << "Error" << endl;
		return;
	}

	for (size_t i = 0; i < Songs->count; i++)
	{
		fprintf(file, "%s", Songs[i].Song->tostring());
	}

	fclose(file);
}

void editSongs(songs* Songs)
{
	if (Songs->count == NULL)
		return;

	int choice{};
	cout << "Enter edit song: " << endl;

	for (size_t i = 0; i < Songs->count; i++)
		cout << i + 1 << '.' << Songs[i].Song->name << endl;
	while (choice < 1 || choice > Songs->count)
		examination(choice);
	
	Songs[choice - 1].Song = addSong();

	FILE* file{};

	fopen_s(&file, "songs.txt", "w");

	if (file == nullptr)
	{
		cout << "Error" << endl;
		return;
	}

	for (size_t i = 0; i < Songs->count; i++)
	{
		fprintf(file, "%s", Songs[i].Song->tostring());
	}

	fclose(file);
}

void bySearch(songs* Songs)
{
	char search[1001]{};
	int len{};
	if (Songs->count == NULL)
		return;

	cin.ignore();
	int choice{};
	cout
		<< "Enter search type: " << endl
		<< "1. Author" << endl
		<< "2. Text" << endl;
	while (choice < 1 || choice > 2)
		examination(choice);

	switch (choice)
	{
		case 1:
			cout << "Enter search author: "; cin.getline(search, 1000);

			while (search[len] != '\0')
				len++;

			for (size_t i = 0; i < Songs->count; i++)
			{
				int yes{};
				for (size_t j = 0; j < len; j++)
				{
					if (Songs[i].Song->author[j] == search[j])
						yes++;
				}

				if (yes == len)
				{
					cout << "Data: " << endl;
					Songs->Song->print();
				}
			}
			break;
		case 2:
			cout << "Enter search text: "; cin.getline(search, 1000);

			while (search[len] != '\0')
				len++;

			for (size_t i = 0; i < Songs->count; i++)
			{
				int yes{};
				for (size_t j = 0; j < len; j++)
				{
					if (Songs[i].Song->text[j] == search[j])
						yes++;
				}

				if (yes == len)
				{
					cout << "Data: " << endl;
					Songs->Song->print();
				}
			}
			break;
	}
}