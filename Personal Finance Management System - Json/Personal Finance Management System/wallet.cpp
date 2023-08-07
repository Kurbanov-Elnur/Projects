#include "Functions.h"

wallet::wallet(personalData& _ownerData, std::string& _ownerEmail, std::string& _ownerPhone, std::string& _currency,
	std::string& _ID, uint16_t& _securityCode, Balance& _balance)
{
	this->ownerData = new personalData(_ownerData);
	this->ID = _ID;
	this->securityCode = new uint16_t{ _securityCode };
	this->ownerEmail = _ownerEmail;
	this->ownerPhone = _ownerPhone;
	this->balance = new Balance{ _balance };
	this->currency = _currency;
}

wallet::wallet()
{
	this->ownerData = new personalData{};
	this->securityCode = new uint16_t{};
	this->balance = new Balance{};
}

wallet::wallet(const wallet& _other)
{
	this->ownerData = new personalData(*_other.ownerData);
	this->ID = _other.ID;
	this->securityCode = new uint16_t(*_other.securityCode);
	this->ownerEmail = _other.ownerEmail;
	this->ownerPhone = _other.ownerPhone;
	this->balance = new Balance(*_other.balance);
	this->currency = _other.currency;
}

void wallet::addCard()
{
	std::string cardNumber{};

	std::string checkData[2]{};

	personalData* person = functions::addPersonalData();
	
	std::cout << "Enter card number: " << std::endl; std::cin >> cardNumber;
	functions::myCheck(cardNumber, std::regex("[0-9]{16}"));

	dateOfExpiry* dateOFExpiry = functions::addDateOfExpiry();

	std::cout << "Enter CVV: " << std::endl; std::cin >> checkData[0];
	functions::myCheck(checkData[0], std::regex("[0-9]{3}"));
	uint16_t CVV = std::stoi(checkData[0]);
	
	std::cout << "Enter balance: " << std::endl; std::cin >> checkData[1];
	functions::myCheck(checkData[1], std::regex("[0-9]{1,4}[.]?[0-9]{1,2}"));
	Balance* balance{};
	balance = new Balance{ *balance->checkAmount(checkData[1]) };

	if (*this->cardsCount == sizeof(this->cards) / sizeof(this->cards[0]))
	{
		card** newCards = new card * [(sizeof(this->cards) / sizeof(this->cards[0])) * 2];

		for (size_t i = 0; i < (sizeof(this->cards) / sizeof(this->cards[0])); ++i) {
			newCards[i] = cards[i];
		}

		delete[] cards;
		this->cards = newCards;
	}

	this->cards[*this->cardsCount] = new card(*person, cardNumber, CVV, *balance, *dateOFExpiry);

	(*this->cardsCount)++;
}

void wallet::cardReplenishment()
{
	std::string choice{};
	if (*this->cardsCount == 0)
		return;

	for (size_t i = 0; i < *this->cardsCount; i++)
		std::cout << i + 1 << ". " << this->cards[i]->getCardNumber() << std::endl;
	do {
		std::cout << "Choice card for replenishment: " << std::endl;
		std::cin >> choice;
		functions::myCheck(choice, std::regex("[0-9]{1,}"));
	} while (std::stoi(choice) > *this->cardsCount || std::stoi(choice) <= 0);

	std::string checkAmount{};
	std::cout << "Enter amount for replenishment: "; std::cin >> checkAmount;
	functions::myCheck(checkAmount, std::regex("[0-9]{1,4}[.]?[0-9]{1,2}"));
	Balance* amount{};
	amount = amount->checkAmount(checkAmount);
	if (*this->cards[std::stoi(choice)]->balance < *amount)
		throw std::invalid_argument("You don't have enough funds!");

	*this->cards[std::stoi(choice) - 1]->balance += *amount;
}

void wallet::walletReplenishment()
{
	std::string checkAmount{};
	std::cout << "Enter amount for replenishment: "; std::cin >> checkAmount;
	functions::myCheck(checkAmount, std::regex("[0-9]{1,4}[.]?[0-9]{1,2}"));
	Balance* amount{};
	amount = amount->checkAmount(checkAmount);
	if (*this->balance < *amount)
		throw std::invalid_argument("You don't have enough funds!");

	*this->balance += *amount;
}

Balance wallet::getBalance() const
{
	return *this->balance;
}

std::string wallet::getID() const
{
	return this->ID;
}

uint16_t wallet::getSecurityCode() const
{
	return *this->securityCode;
}