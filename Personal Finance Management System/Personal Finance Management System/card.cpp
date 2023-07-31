#include "card.h"

card::card(personalData& _ownerData, std::string& _cardNumber, uint16_t& _CVV, uint16_t& _balance, dateOfExpiry& _dataOfExpiry)
{
	this->ownerData = new personalData (_ownerData);
	this->cardNumber = _cardNumber ;
	this->CVV = new uint16_t{ _CVV };
	this->dateExpiry = new dateOfExpiry (_dataOfExpiry);
	this->balance = new uint16_t{ _balance };
}

void card::addBalance(uint16_t amount)
{
	this->balance += amount;
}

std::string card::getCardNumber() const
{
	return this->cardNumber;
}

uint16_t card::getCVV() const
{
	return *this->CVV;
}

uint16_t card::getBalance() const
{
	return *this->balance;
}

//card::~card()
//{
//	delete this->balance;
//	delete this->CVV;
//	delete this->ownerData;
//	delete this->dateExpiry;
//}