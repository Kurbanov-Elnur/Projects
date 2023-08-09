#pragma warning(disable : 4996)
#include "Functions.h"
#include <ctime>

std::string functions::checkWallets(wallet** _wallets, uint16_t& count)
{
	std::string currentWallet = "0";
	if (_wallets[0] == nullptr)
	{
		_wallets[0] = functions::addWallet();
		functions::saveInFile(*_wallets[0], "wallets");
		count++;
		currentWallet = "1";
		system("cls");
	}
	else
	{
		while (true)
		{
			std::cout << "Enter current wallet: " << std::endl;
			for (size_t i = 0; i < count; i++)
			{
				std::cout << i + 1 << ". " << _wallets[i]->getID() << std::endl;
			}
			while (std::stoi(currentWallet) <= 0 || std::stoi(currentWallet) > count)
			{
				std::cin >> currentWallet;
				functions::myCheck(currentWallet, std::regex("[0-9]{1,}"));
			}
			std::string security{};
			std::cout << "Enter security code: "; std::cin >> security;
			functions::myCheck(security, std::regex("[0-9]{4}"));

			if (_wallets[std::stoi(currentWallet) - 1]->getSecurityCode() == std::stoi(security))
				break;
			system("cls");
			continue;
		}
	}

	return currentWallet;
}

void functions::formationOfRatings(wallet* wallet)
{
	std::string startDay = "0", startMonth = "0", endDay = "0", endMonth = "0";

	do {
		std::cout << "Enter start day: "; std::cin >> startDay;
		std::cout << "Enter start month: "; std::cin >> startMonth;
		functions::myCheck(startDay, std::regex("[0-9]{1,2}"));
		functions::myCheck(startMonth, std::regex("[0-9]{1,2}"));
	} while (std::stoi(startDay) <= 0 || std::stoi(startDay) > 31 || std::stoi(startMonth) <= 0 || std::stoi(startMonth) > 12);

	do{
		std::cout << "Enter end day: "; std::cin >> endDay;
		std::cout << "Enter end month: "; std::cin >> endMonth;
		functions::myCheck(endDay, std::regex("[0-9]{1,2}"));
		functions::myCheck(endMonth, std::regex("[0-9]{1,2}"));
	} while (std::stoi(endDay) <= 0 || std::stoi(endDay) > 31 || std::stoi(endMonth) <= 0 || std::stoi(endMonth) > 12);

	Balance amounts[8]{};
	for (size_t i = 0; i < *wallet->tranasctionCount; i++)
	{
		if(wallet->Transactions[i]->getSendDay() >= std::stoi(startDay) && wallet->Transactions[i]->getSendDay() <= std::stoi(endDay) &&
			wallet->Transactions[i]->getSendMonth() >= std::stoi(startMonth) && wallet->Transactions[i]->getSendMonth() <= std::stoi(endMonth))
		{
			amounts[wallet->Transactions[i]->getCategory() - 1] += *wallet->Transactions[i]->getSendAmount();
		}
	}

	for (size_t i = 0; i < 8; i++)
	{
		std::cout << wallet->Transactions[0]->categories[i] << ' ' << amounts[i] << std::endl;
	}
}

void functions::top3cost(wallet* wallets)
{
	std::string startDay = "0", startMonth = "0", endDay = "0", endMonth = "0";

	do {
		std::cout << "Enter start day: "; std::cin >> startDay;
		std::cout << "Enter start month: "; std::cin >> startMonth;
		functions::myCheck(startDay, std::regex("[0-9]{1,2}"));
		functions::myCheck(startMonth, std::regex("[0-9]{1,2}"));
	} while (std::stoi(startDay) <= 0 || std::stoi(startDay) > 31 || std::stoi(startMonth) <= 0 || std::stoi(startMonth) > 12);

	do {
		std::cout << "Enter end day: "; std::cin >> endDay;
		std::cout << "Enter end month: "; std::cin >> endMonth;
		functions::myCheck(endDay, std::regex("[0-9]{1,2}"));
		functions::myCheck(endMonth, std::regex("[0-9]{1,2}"));
	} while (std::stoi(endDay) <= 0 || std::stoi(endDay) > 31 || std::stoi(endMonth) <= 0 || std::stoi(endMonth) > 12);

	Balance top3[3]{};

	for (size_t i = 0; i < *wallets->tranasctionCount; i++)
	{
		if (wallets->Transactions[i]->getSendDay() >= std::stoi(startDay) && wallets->Transactions[i]->getSendDay() <= std::stoi(endDay) &&
			wallets->Transactions[i]->getSendMonth() >= std::stoi(startMonth) && wallets->Transactions[i]->getSendMonth() <= std::stoi(endMonth))
		{
			if (*wallets->Transactions[i]->getSendAmount() > top3[0])
			{
				top3[2] = top3[1];
				top3[1] = top3[0];
				top3[0] = *wallets->Transactions[i]->getSendAmount();
			}
			else if (*wallets->Transactions[i]->getSendAmount() > top3[1])
			{
				top3[2] = top3[1];
				top3[1] = *wallets->Transactions[i]->getSendAmount();
			}
			else if (*wallets->Transactions[i]->getSendAmount() > top3[2])
				top3[2] = *wallets->Transactions[i]->getSendAmount();
		}
	}

	for (size_t i = 0; i < 3; i++)
	{
		std::cout << i + 1 << ' ' << top3[i] << std::endl;
	}
}

void functions::top3category(wallet* wallets)
{
	std::string startDay = "0", startMonth = "0", endDay = "0", endMonth = "0";

	do {
		std::cout << "Enter start day: "; std::cin >> startDay;
		std::cout << "Enter start month: "; std::cin >> startMonth;
		functions::myCheck(startDay, std::regex("[0-9]{1,2}"));
		functions::myCheck(startMonth, std::regex("[0-9]{1,2}"));
	} while (std::stoi(startDay) <= 0 || std::stoi(startDay) > 31 || std::stoi(startMonth) <= 0 || std::stoi(startMonth) > 12);

	do {
		std::cout << "Enter end day: "; std::cin >> endDay;
		std::cout << "Enter end month: "; std::cin >> endMonth;
		functions::myCheck(endDay, std::regex("[0-9]{1,2}"));
		functions::myCheck(endMonth, std::regex("[0-9]{1,2}"));
	} while (std::stoi(endDay) <= 0 || std::stoi(endDay) > 31 || std::stoi(endMonth) <= 0 || std::stoi(endMonth) > 12);

	Balance amounts[8]{};

	for (size_t i = 0; i < *wallets->tranasctionCount; i++)
	{
		if (wallets->Transactions[i]->getSendDay() >= std::stoi(startDay) && wallets->Transactions[i]->getSendDay() <= std::stoi(endDay) &&
			wallets->Transactions[i]->getSendMonth() >= std::stoi(startMonth) && wallets->Transactions[i]->getSendMonth() <= std::stoi(endMonth))
		{
			amounts[wallets->Transactions[i]->getCategory() - 1] += *wallets->Transactions[i]->getSendAmount();
		}
	}

	int index{};
	for (size_t i = 0; i < 3; i++)
	{
		Balance max{};
		for (size_t j = 0; j < 8; j++)
		{
			if (amounts[j] > max)
			{
				max = amounts[j];
				index = j;
			}
		}
		amounts[index] = Balance{};
		std::cout << i + 1 << '.' << wallets->Transactions[0]->categories[index] << std::endl;
	}
}

int functions::getCurrentYear()
{
	std::time_t now = std::time(nullptr);
	std::tm* timeinfo = std::localtime(&now);
	int currentYear = timeinfo->tm_year + 1900;

	return currentYear;
}

void functions::myCheck(std::string& str, std::regex regexCheck)
{
	while (true)
	{
		if (!std::regex_match(str, regexCheck)) {
			std::cout << "Invalid input!" << std::endl;
			std::cout << "Please re-enter: " << std::endl; std::cin >> str;
			continue;
		}
		break;
	}
}

wallet* functions::addWallet()
{
	std::string ownerEmail{}, ownerPhone{}, currency{}, ID{};

	std::string checkData[3];

	personalData* person = functions::addPersonalData();

	std::cout << "Enter owner email: " << std::endl; std::cin >> ownerEmail;
	functions::myCheck(ownerEmail, std::regex(R"(([a-zA-Z0-9](.|_)?)+([a-zA-Z0-9])+@([a-zA-Z0-9])+((.)[a-zA-Z]{2,})+)"));

	std::cout << "Enter owner phone: " << std::endl; std::cin >> ownerPhone;
	functions::myCheck(ownerPhone, std::regex("^[+]994[0-9]{9}$"));

	std::cout << "Enter wallet currency: " << std::endl; std::cin >> currency;
	functions::myCheck(currency, std::regex("[A-Z]{2,20}"));

	std::cout << "Enter wallet id: " << std::endl; std::cin >> ID;
	functions::myCheck(ID, std::regex("[0-9]{8}"));

	std::cout << "Enter security code: "; std::cin >> checkData[1];
	functions::myCheck(checkData[1], std::regex("[0-9]{4}"));
	uint16_t securityCode = std::stoi(checkData[1]);

	std::cout << "Enter balance: " << std::endl; std::cin >> checkData[2];
	functions::myCheck(checkData[2], std::regex("[0-9]{1,4}[.]?([0-9]{1,2})?"));
	Balance* balance{};
	balance = balance->checkAmount(checkData[2]);

	wallet* w = new wallet(*person, ownerEmail, ownerPhone, currency, ID, securityCode, *balance);

	return w;
}

Transaction* functions::addTransaction()
{
	std::string senderID, recipientID{}, checkSendDay{}, checkSendMonth{}, checkSendYear{}, checkSendAmount{}, checkCategory{};
	uint16_t sendDay{}, sendMonth{}, sendYear = functions::getCurrentYear(), category{};
	Balance* sendAmount{};

	std::cout << "Enter sender ID: " << std::endl; std::cin >> senderID;
	functions::myCheck(senderID, std::regex("[0-9]{8}|[0-9]{16}"));

	do {
		std::cout << "Enter recipient ID: " << std::endl; std::cin >> recipientID;
		functions::myCheck(recipientID, std::regex("[0-9]{8}|[0-9]{16}"));
	} while (senderID == recipientID);

	while (sendDay > 31 || sendDay <= 0)
	{
		std::cout << "Enter send day: " << std::endl; std::cin >> checkSendDay;
		functions::myCheck(checkSendDay, std::regex("[0-9]{1,2}"));
		sendDay = std::stoi(checkSendDay);
	}

	while (sendMonth > 12 || sendMonth <= 0)
	{
		std::cout << "Enter send month: " << std::endl; std::cin >> checkSendMonth;
		functions::myCheck(checkSendMonth, std::regex("[0-9]{1,2}"));
		sendMonth = std::stoi(checkSendMonth);
	}

	std::cout << "Enter amount for replenishment: "; std::cin >> checkSendAmount;
	functions::myCheck(checkSendAmount, std::regex("[0-9]{1,4}[.]?[0-9]{1,2}"));
	sendAmount = new Balance(*sendAmount->checkAmount(checkSendAmount));

	std::cout
		<< "Enter category: " << std::endl
		<< "1. Food" << std::endl
		<< "2. Utilities" << std::endl
		<< "3. Transport" << std::endl
		<< "4. Health" << std::endl
		<< "5. Education" << std::endl
		<< "6. Entertaiments" << std::endl
		<< "7. Clothes" << std::endl
		<< "8. Communications" << std::endl;
	std::cin >> checkCategory;
	functions::myCheck(checkCategory, std::regex("[1-8]"));
	category = std::stoi(checkCategory);
		 
	Transaction* t = new Transaction(senderID, recipientID, sendDay, sendMonth, sendYear, *sendAmount, category);

	return t;
}

personalData* functions::addPersonalData()
{
	std::string name{}, surname{}, patronomic{}, checkDayOfBirth{}, checkMonthOfBirth{}, checkYearOfBirth{};
	uint16_t dayOfBirth{}, monthOfBirth{}, yearOfBirth{};

	std::cout << "Enter the owner's name: " << std::endl; std::cin >> name;
	functions::myCheck(name, std::regex("[A-Z]{2,20}"));

	std::cout << "Enter owner surname: " << std::endl; std::cin >> surname;
	functions::myCheck(surname, std::regex("[A-Z]{2,20}"));

	std::cout << "Enter the patronymic of the owner: " << std::endl; std::cin >> patronomic;
	functions::myCheck(patronomic, std::regex("[A-Z]{2,20}"));

	while(dayOfBirth == 0 || dayOfBirth > 31)
	{
		std::cout << "Enter the owner's birthday: " << std::endl; std::cin >> checkDayOfBirth;
		functions::myCheck(checkDayOfBirth, std::regex("[0-9]{1,2}"));
		dayOfBirth = std::stoi(checkDayOfBirth);
	}

	while (monthOfBirth == 0 || monthOfBirth > 12)
	{
		std::cout << "Enter the owner's month of birth: " << std::endl; std::cin >> checkMonthOfBirth;

		functions::myCheck(checkMonthOfBirth, std::regex("[0-9]{1,2}"));
		monthOfBirth = std::stoi(checkMonthOfBirth);
	}

	int currentYear = functions::getCurrentYear();

	while (yearOfBirth == 0 || currentYear - yearOfBirth < 18)
	{
		std::cout << "Enter the owner's year of birth: " << std::endl; std::cin >> checkYearOfBirth;
		functions::myCheck(checkYearOfBirth, std::regex("[0-9]{4}"));
		yearOfBirth = std::stoi(checkYearOfBirth);
	}
	
	personalData* p = new personalData(name, surname, patronomic, dayOfBirth, monthOfBirth, yearOfBirth);

	return p;
}

dateOfExpiry* functions::addDateOfExpiry()
{
	uint16_t monthOfExpiry{}, yearOfExpiry{};
	std::string checkMonthOfExpiry{}, checkYearOfExpiry{};

	int currentYear = functions::getCurrentYear();

	while (monthOfExpiry == 0 || monthOfExpiry > 12)
	{
		std::cout << "Enter month of expiry: " << std::endl; std::cin >> checkMonthOfExpiry;
		functions::myCheck(checkMonthOfExpiry, std::regex("[0-9]{1,2}"));
		monthOfExpiry = std::stoi(checkMonthOfExpiry);
	}

	while (yearOfExpiry == 0 || currentYear - yearOfExpiry > 10 )
	{
		std::cout << "Enter year of expiry: " << std::endl; std::cin >> checkYearOfExpiry;
		functions::myCheck(checkYearOfExpiry, std::regex("[0-9]{4}"));
		yearOfExpiry = std::stoi(checkYearOfExpiry);
	}

	dateOfExpiry* d = new dateOfExpiry (monthOfExpiry, yearOfExpiry);

	return d;
}