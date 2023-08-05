#include "card.h"

card::card(personalData& _ownerData, std::string& _cardNumber, uint16_t& _CVV, Balance& _balance, dateOfExpiry& _dataOfExpiry)
{
	this->ownerData = new personalData (_ownerData);
	this->cardNumber = _cardNumber ;
	this->CVV = new uint16_t{ _CVV };
	this->dateExpiry = new dateOfExpiry (_dataOfExpiry);
	this->balance = new Balance{ _balance };
}

std::string card::getCardNumber() const
{
	return this->cardNumber;
}

uint16_t card::getCVV() const
{
	return *this->CVV;
}

Balance card::getBalance() const
{
	return *this->balance;
}