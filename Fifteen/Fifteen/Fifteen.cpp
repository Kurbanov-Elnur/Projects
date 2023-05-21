#include <iostream>
#include <Windows.h>
using namespace std;

void getClick(int& x, int& y)
{
	HANDLE hConsoleIn = GetStdHandle(STD_INPUT_HANDLE);
	INPUT_RECORD inputRec;
	DWORD events;
	COORD coord;
	bool clicked = false;

	DWORD fdwMode = ENABLE_EXTENDED_FLAGS | ENABLE_WINDOW_INPUT | ENABLE_MOUSE_INPUT;
	SetConsoleMode(hConsoleIn, fdwMode);

	while (!clicked) {

		ReadConsoleInput(hConsoleIn, &inputRec, 1, &events);

		if (inputRec.EventType == MOUSE_EVENT) {
			if (inputRec.Event.MouseEvent.dwButtonState == FROM_LEFT_1ST_BUTTON_PRESSED) {
				coord = inputRec.Event.MouseEvent.dwMousePosition;
				x = coord.X;
				y = coord.Y;
				clicked = true;
			}
		}
		if (GetAsyncKeyState(VK_ESCAPE)) {
			cout << "Exiting" << endl;
			break;
		}
		else if (inputRec.EventType == KEY_EVENT) {
		}
	}
}

void createField(int*& field, int y, int size)
{
	field = new int[size] {};
	int* count = new int[size] {};

	srand(time(0));
	int r{};
	for (size_t i = 0; i < size - 1; i++)
	{
		r = rand() % (size - 1) + 1;
		if (count[r - 1] < 1)
		{
			field[i] = r;
			count[r - 1]++;
		}
		else
			i--;
	}
}

int main()
{
	int* field{};
	int x{}, y{};

	cout
		<< "Enter Field size: " << endl
		<< "1. 3X3" << endl
		<< "2. 4X4" << endl;
	getClick(x, y);

	while (y < 1 && y > 3)
	{
		cout << "Invalid input, re-enter: " << endl;
		getClick(x, y);
	}

	while (y != 1 && y != 2)
		getClick(x, y);
	int size{};
	if (y == 1)
		size = 9;
	else if (y == 2)
		size = 16;

	createField(field, y, size);
	
	int numbers16[17]{80, 160, 240, 320, 81, 161, 241, 321, 82, 162, 242, 322, 83, 163, 243, 323};
	int numbers9[10]{ 80, 160, 240, 81, 161, 241, 82, 162, 242};
	bool close = true;
	int time1 = time(0);
	int move{};

	while (close)
	{
		system("cls");
		for (size_t i = 0; i < size; i++)
		{
			cout << '\t';
			if (size == 9)
			{
				cout << field[i] << ' ';
				if ((i + 1) % 3 == 0)
					cout << endl;
			}
			else
			{
				cout << field[i] << ' ';
				if ((i + 1) % 4 == 0)
					cout << endl;
			}
		}
		int choice{};
		int m{}, i{};
		while (m == 0)
		{
			getClick(x, y);
			choice = (x *= 10) + y;
			int z{};
			if (size == 9)
			{
				for (; numbers9[z] != 0; z++)
				{
					if (choice == numbers9[z])
					{
						m++;
						i = z;
						break;
					}
				}
			}
			else
			{
				for (; numbers16[z] != 0; z++)
				{
					if (choice == numbers16[z] || choice == numbers16[z] + 10)
					{
						m++;
						i = z;
						break;
					}
				}
			}
		}
		if (size == 9)
		{
			if (field[i + 1] == 0)
			{
				int r = field[i];
				field[i] = field[i + 1];
				field[i + 1] = r;
			}
			else if (field[i - 1] == 0)
			{
				int r = field[i];
				field[i] = field[i - 1];
				field[i - 1] = r;
			}
			else if (field[i + 3] == 0)
			{
				int r = field[i];
				field[i] = field[i + 3];
				field[i + 3] = r;
			}
			else if (field[i - 3] == 0)
			{
				int r = field[i];
				field[i] = field[i - 3];
				field[i - 3] = r;
			}
		}
		else
		{
			if (field[i + 1] == 0)
			{
				int r = field[i];
				field[i] = field[i + 1];
				field[i + 1] = r;
			}
			else if (field[i - 1] == 0)
			{
				int r = field[i];
				field[i] = field[i - 1];
				field[i - 1] = r;
			}
			else if (field[i + 4] == 0)
			{
				int r = field[i];
				field[i] = field[i + 4];
				field[i + 4] = r;
			}
			else if (field[i - 4] == 0)
			{
				int r = field[i];
				field[i] = field[i - 4];
				field[i - 4] = r;
			}
		}
		int stop{};
		for (size_t i = 0; i < size - 1; i++)
		{
			if (field[i] == i + 1)
				stop++;
		}
		if (stop == size - 1)
			close = false;
	}

	int time2 = time(0) - time1;
	cout << "You did it in " << time2 << "seconds!" << endl;
	cout << "For " << move << " moves";

	return 0;
}