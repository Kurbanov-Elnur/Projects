#include "Functions.h"

int main()
{
	uint16_t walletsCount{};
	wallet** wallets = new wallet*[10]{};

	if (functions::loadFromFile(wallets, "wallets", walletsCount))
	{
		functions::loadFromFile(wallets[0]->cards, "cards", *wallets[0]->cardsCount);
		functions::loadFromFile(wallets[0]->Transactions, "transaction", *wallets[0]->tranasctionCount);
	}

	std::string choice{}, currentWallet = functions::checkWallets(wallets, walletsCount);

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
				wallets[std::stoi(currentWallet) - 1]->addCard();
				functions::saveInFile(*wallets[std::stoi(currentWallet) - 1]->cards[*wallets[std::stoi(currentWallet) - 1]->cardsCount - 1], "cards");
				system("cls");
				break;
			case 3:
				wallets[std::stoi(currentWallet) - 1]->walletReplenishment();
				system("cls");
				break;
			case 4:
				try {
					wallets[std::stoi(currentWallet) - 1]->cardReplenishment();
				}
				catch (std::exception& e)
				{
					std::cout << e.what() << std::endl;
				}
				system("cls");
				break;
			case 5:
				if (*wallets[std::stoi(currentWallet) - 1]->tranasctionCount == sizeof(wallets[std::stoi(currentWallet) - 1]->Transactions) / sizeof(wallets[std::stoi(currentWallet) - 1]->Transactions[0]))
				{
					Transaction** newCards = new Transaction * [(sizeof(wallets[std::stoi(currentWallet) - 1]->Transactions) / sizeof(wallets[std::stoi(currentWallet) - 1]->Transactions[0])) * 2];

					for (size_t i = 0; i < sizeof(wallets[std::stoi(currentWallet) - 1]->Transactions) / sizeof(wallets[std::stoi(currentWallet) - 1]->Transactions[0]); ++i) {
						newCards[i] = wallets[std::stoi(currentWallet) - 1]->Transactions[i];
					}

					delete[] wallets[std::stoi(currentWallet) - 1]->Transactions;
					wallets[std::stoi(currentWallet) - 1]->Transactions = newCards;
				}
				wallets[std::stoi(currentWallet) - 1]->Transactions[*wallets[std::stoi(currentWallet) - 1]->tranasctionCount] = functions::addTransaction();
				functions::saveInFile(*wallets[std::stoi(currentWallet) - 1]->Transactions[*wallets[std::stoi(currentWallet) - 1]->tranasctionCount], "transaction");
				(*wallets[std::stoi(currentWallet) - 1]->tranasctionCount)++;
				system("cls");
				break;
			case 6:
				functions::formationOfRatings(*wallets[std::stoi(currentWallet) - 1]);
				system("cls");
				break;
			case 7:
				functions::top3cost(*wallets[std::stoi(currentWallet) - 1]);
				break;
			case 8:
				functions::top3category(*wallets[std::stoi(currentWallet) - 1]);
				break;
		}
	}

	return 0;
}