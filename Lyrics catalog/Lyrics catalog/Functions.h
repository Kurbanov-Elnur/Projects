#include <iostream>
using namespace std;

struct song
{
	char* name = new char[33] {};
	char* author = new char[33] {};
	char* text = new char[1003] {};
	char* year = new char[7] {};

	char* tostring()
	{
		char* str = new char[1500] {};
		sprintf_s(str, 1500, "%s\n%s\n%s\n%s\n", name, author, text, year);
		return str;
	}

	void print()
	{
		cout
			<< "Name: " << name << endl
			<< "Author: " << author << endl
			<< "Text: " << text << endl
			<< "Year: " << year << endl;
	}
};

struct songs
{
	char* name = new char[31] {};
	song* Song{};
	int count{};


	void savetoFile()
	{
		FILE* file{};
		fopen_s(&file, "songs.txt", "a+");

		if (file == nullptr)
		{
			cout << "Error" << endl;
			return;
		}

		fprintf(file, "%s", Song->tostring());

		fclose(file);
	}

};

void examination(int& obyekt);
songs* createSongs(songs*& Songs);
char* loadSongs(songs*& Songs);
int length(char* obyekt);
song* addSong();
void deleteSong(songs* Songs);
void editSongs(songs* Songs);
void bySearchAuthor(songs* Songs);