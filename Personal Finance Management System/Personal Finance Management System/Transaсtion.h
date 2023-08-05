#pragma once
#include <iostream>
#include "Balance.h"

class Transaction
{
private:
	std::string senderID{};
	std::string recipientID{};
	uint16_t* sendDay{};
	uint16_t* sendMonth{};
	uint16_t* sendYear{};
	Balance* sendAmount{};
	uint16_t* category{};
public:
	std::string categories[8]{ "Food", "Utilities", "Transport", "Health", "Education", "Entertainments", "Clothes", "Communications" };

	Transaction(std::string&, std::string&, uint16_t&, uint16_t&, uint16_t&, Balance&, uint16_t&);
	
	friend std::ostream& operator << (std::ostream& os, const Transaction _Transaction)
	{
		if (typeid(os) == typeid(std::ofstream))
		{
			os
				<< _Transaction.senderID << std::endl
				<< _Transaction.recipientID << std::endl
				<< *_Transaction.sendDay << std::endl
				<< *_Transaction.sendMonth << std::endl
				<< *_Transaction.sendYear << std::endl
				<< *_Transaction.sendAmount
				<< *_Transaction.category << std::endl;
			return os;
		}
		os
			<< "Sender ID: " << _Transaction.senderID << std::endl
			<< "Recipient ID: " << _Transaction.recipientID << std::endl
			<< "Date transaction: " << *_Transaction.sendDay << '.' << *_Transaction.sendMonth << '.' << *_Transaction.sendYear << std::endl
			<< "Send amount: " << *_Transaction.sendAmount << std::endl
			<< "Category: " << _Transaction.categories[*_Transaction.category - 1] << std::endl;
		return os;
	}

	friend std::istream& operator>>(std::istream& is, Transaction& _Transaction)
	{
		is >> _Transaction.senderID >> _Transaction.recipientID >> *_Transaction.sendDay >> *_Transaction.sendMonth >> *_Transaction.sendYear >>
			*_Transaction.sendAmount >> *_Transaction.category;
		return is;
	}

	std::string getSenderID() const;
	std::string getRecipientID() const;
	uint16_t getSendDay() const;
	uint16_t getSendMonth() const;
	uint16_t getSendYear() const;
	Balance* getSendAmount() const;
	uint16_t getCategory() const;
};

