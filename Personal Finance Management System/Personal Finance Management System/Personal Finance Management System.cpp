#include "Functions.h"

int main()
{
	uint16_t walletsCount{};
	wallet** wallets = new wallet*[10]{};

	std::string choice{}, currentWallet = "0";

	if (wallets[0] == nullptr)
	{
		wallets[0] = functions::addWallet();
		functions::saveInFile(*wallets[0], "wallets");
		walletsCount++;
		system("cls");
	}
	else
	{
		while (true)
		{
			std::cout << "Enter current wallet: " << std::endl;
			for (size_t i = 0; i < walletsCount; i++)
			{
				std::cout << i + 1 << ". " << wallets[i]->getID() << std::endl;
			}
			while (std::stoi(currentWallet) <= 0 || std::stoi(currentWallet) > walletsCount)
			{
				std::cin >> currentWallet;
				functions::myCheck(currentWallet, std::regex("[0-9]{1,}"));
			}
			std::string security{};
			std::cout << "Enter security code: "; std::cin >> security;
			functions::myCheck(security, std::regex("[0-9]{4}"));

			if (wallets[std::stoi(currentWallet)]->getSecurityCode() == std::stoi(security))
				break;
			system("cls");
			continue;
		}
	}

	while (true)
	{
		std::cout
			<< "1. Add Wallet" << std::endl
			<< "2. Add card" << std::endl
			<< "3. Wallet Replenishment" << std::endl
			<< "4. Card Replenishment" << std::endl
			<< "5. Add Transaction" << std::endl
			<< "6. Generating reports" << std::endl
			<< "7. Formation of ratings by maximum amounts" << std::endl
			<< "8. Formation of ratings by maximum category" << std::endl;
		std::cin >> choice;
		functions::myCheck(choice, std::regex("[1-8]{1}"));

		switch (std::stoi(choice))
		{
			case 1:
				wallets[walletsCount] = functions::addWallet();
				functions::saveInFile(*wallets[walletsCount], "wallets");
				walletsCount++;
				system("cls");
				break;
			case 2:
				wallets[std::stoi(currentWallet)]->addCard();
				functions::saveInFile(*wallets[std::stoi(currentWallet)]->cards[*wallets[std::stoi(currentWallet)]->cardsCount - 1], "cards");
				system("cls");
				break;
			case 3:
				wallets[std::stoi(currentWallet)]->walletReplenishment();
				system("cls");
				break;
			case 4:
				try {
					wallets[std::stoi(currentWallet)]->cardReplenishment();
				}
				catch (std::exception& e)
				{
					std::cout << e.what() << std::endl;
				}
				system("cls");
				break;
			case 5:
				wallets[std::stoi(currentWallet)]->Transactions[*wallets[std::stoi(currentWallet)]->tranasctionCount] = functions::addTransaction();
				functions::saveInFile(*wallets[std::stoi(currentWallet)]->Transactions[*wallets[std::stoi(currentWallet)]->tranasctionCount], "transaction");
				(*wallets[std::stoi(currentWallet)]->tranasctionCount)++;
				system("cls");
				break;
			case 6:
				functions::formationOfRatings(*wallets[std::stoi(currentWallet)]);
				system("cls");
				break;
			case 7:
				functions::top3cost(*wallets[std::stoi(currentWallet)]);
				break;
			case 8:
				functions::top3category(*wallets[std::stoi(currentWallet)]);
				break;
		}
	}

	return 0;
}