#include "card.h"

card::card(personalData& _ownerData, std::string& _cardNumber, uint16_t& _CVV, float& _balance, dateOfExpiry& _dataOfExpiry)
{
	this->ownerData = new personalData (_ownerData);
	this->cardNumber = _cardNumber ;
	this->CVV = new uint16_t{ _CVV };
	this->dateExpiry = new dateOfExpiry (_dataOfExpiry);
	this->balance = new float{ _balance };
}

void card::setBalance(float newBalance)
{
	delete this->balance;
	this->balance = new float{ newBalance };
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