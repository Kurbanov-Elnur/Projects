#include "Functions.h"

wallet::wallet(personalData& _ownerData, std::string& _ownerEmail, std::string& _ownerPhone, std::string& _currency,
	uint16_t& _ID, uint16_t& _securityCode, uint16_t& _balance)
{
	this->ownerData = new personalData(_ownerData);
	this->ID = new uint16_t{ _ID };
	this->securityCode = new uint16_t{ _securityCode };
	this->ownerEmail = _ownerEmail;
	this->ownerPhone = _ownerPhone;
	this->balance = new uint16_t{ _balance };
	this->currency = _currency;
}

void wallet::addCard()
{
	std::string cardNumber{};

	std::string checkData[2]{};

	personalData person = functions::addPersonalData();
	
	std::cout << "Enter card number: " << std::endl; std::cin >> cardNumber;
	functions::myCheck(cardNumber, std::regex("[0-9]{16}"));

	dateOfExpiry dateOFExpiry = functions::addDateOfExpiry();

	std::cout << "Enter CVV: " << std::endl; std::cin >> checkData[0];
	functions::myCheck(checkData[0], std::regex("[0-9]{3}"));
	uint16_t CVV = std::stoi(checkData[0]);
	
	std::cout << "Enter balance: " << std::endl; std::cin >> checkData[1];
	functions::myCheck(checkData[1], std::regex("[0-9]{1,6}"));
	uint16_t balance = std::stoi(checkData[1]);

	this->cards[*this->cardsCount] = new card(person, cardNumber, CVV, balance, dateOFExpiry);

	this->cardsCount++;
}

void wallet::cardReplenishment()
{
	std::string choice{};

	for (size_t i = 0; i < *this->cardsCount; i++)
		std::cout << i + 1 << ". " << this->cards[i]->getCardNumber() << std::endl;
	do {
		std::cout << "Choice card for replenishment: " << std::endl;
		std::cin >> choice;
		functions::myCheck(choice, std::regex("[0 - 9]{1,}"));
	} while (std::stoi(choice) > *this->cardsCount || std::stoi(choice) <= 0);

	std::string amount{};
	std::cout << "Enter amount for replenishment: "; std::cin >> amount;
	functions::myCheck(amount, std::regex("[0-9]{1,6}"));

	this->cards[std::stoi(choice)]->addBalance(std::stoi(amount));
}

uint16_t wallet::getBalance() const
{
	return *this->balance;
}

uint16_t wallet::getID() const
{
	return *this->ID;
}

uint16_t wallet::getSecurityCode() const
{
	return *this->securityCode;
}

//wallet::~wallet()
//{
//	delete this->ownerData;
//	delete this->ID;
//	delete this->securityCode;
//	delete this->balance;
//	delete this->transactionLimit;
//	delete this->dailyLimit;
//	delete this->balance;
//	for (int i = 0; i < *this->cardsCount; ++i) {
//		delete cards[i];
//	}
//	delete[] cards;
//	delete this->cardsCount;
//}